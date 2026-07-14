using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseAgent : MonoBehaviour
{
    [SerializeField] private EnemyState currentState = EnemyState.Idle;
    [SerializeField] private EnemyHealth enemyHealth;

    [SerializeField] private Transform target;
    [SerializeField] private PlayerHealth targetHealth;

    [SerializeField] private float targetRefreshInterval = 0.15f;

    [SerializeField] private float minTargetMoveDistance = 0.1f;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float chaseDistance = 20.0f;
    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackRotateSpeed = 8.0f;

    [SerializeField] private EnemyAttackEventReceiver eventReceiver;

    [SerializeField] private EnemyAttackType attackType = EnemyAttackType.Melee;

    private float attackTimer;
    public bool IsAttacking { get; private set; }

    private Vector3 lastTargetPosition;
    private float refreshTimer;

    public bool IsChasing { get; private set; }

    public EnemyState CurrentState { get { return currentState; } }
    public EnemyAttackType AttackType { get { return attackType; } }

    private void Awake()
    {
        lastTargetPosition = transform.position;

        GameObject targetObject = GameObject.FindGameObjectWithTag("Player");
        if(targetObject != null)
        {
            target = targetObject.transform;
            targetHealth = targetObject.GetComponent<PlayerHealth>();

            if(targetHealth != null && eventReceiver != null)
            {
                eventReceiver.SetTargetHealth(targetHealth);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAttackTimer();
        UpdateStateByRules();
        ExecuteCurrentState();
    }

    bool CanChaseTarget()
    {
        if(agent == null)
        {
            return false;
        }

        if(agent.enabled == false)
        {
            return false;
        }

        if(agent.isOnNavMesh == false)
        {
            return false;
        }

        if(target == null)
        {
            return false;
        }

        if(targetHealth == null || targetHealth.IsDead == true)
        {
            return false;
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if(distance >= chaseDistance)
        {
            return false;
        }

        return true;
    }

    void UpdateDestinationIfNeeded()
    {
        Vector3 targetPosition = target.position;

        float minSqrDistance = minTargetMoveDistance * minTargetMoveDistance;

        // 마지막 목적지와 현재 위치의 제곱 거리.
        float targetsqrMoveDistance = (targetPosition - lastTargetPosition).sqrMagnitude;

        if(targetsqrMoveDistance < minSqrDistance && agent.hasPath == true)
        {
            IsChasing = true;
            return;
        }

        lastTargetPosition = targetPosition;
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
        IsChasing = true;
    }

    void StopAgentSafely()
    {
        if(agent == null || agent.enabled == false || agent.isOnNavMesh == false)
        {
            return;
        }

        agent.isStopped = true;
        agent.ResetPath();
    }

    void TryAttack()
    {
        if(attackTimer > 0.0f)
        {
            return;
        }

        attackTimer = attackCooldown;

        if(targetHealth.IsDead == true)
        {
            return;
        }

        //targetHealth.TakeDamage(attackDamage);
    }

    /// <summary>
    /// 목표를 바라보게 만드는 함수.
    /// </summary>
    void RotateToTarget()
    {
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0.0f;

        // Quaternion.LookRotation : 지정된 방향으로 회전시켜주는 함수.
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, attackRotateSpeed * Time.deltaTime);
    }

    void StopForAttack()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }

    bool IsTargetInAttackRange()
    {
        Vector3 targetOffset = target.position - transform.position;
        targetOffset.y = 0.0f;  // 높이 차이를 거리 계산에서 제외.

        float targetSqrDistance = targetOffset.sqrMagnitude;
        float attacksqrDistance = attackDistance * attackDistance;       

        return targetSqrDistance <= attacksqrDistance;

        //float targetDistance = targetOffset.magnitude;
        //return targetDistance <= attackDistance;
    }

    void UpdateAttackTimer()
    {
        if(attackTimer <= 0.0f)
        {
            return;
        }

        attackTimer -= Time.deltaTime;
    }

    void HandleDeadState()
    {
        StopAgentSafely();
        IsChasing = false;
        IsAttacking = false;
    }

    void HandleAttackState()
    {
        StopForAttack();
        RotateToTarget();
        TryAttack();
    }

    void HandleChaseState()
    {
        refreshTimer -= Time.deltaTime;
        if(refreshTimer <= 0.0f)
        {
            refreshTimer = targetRefreshInterval;
            UpdateDestinationIfNeeded();
        }
    }

    void HandleIdleState()
    {
        // 아직은 아무 처리도 하지 않음.
    }

    void ExecuteCurrentState()
    {
        if(currentState == EnemyState.Idle)
        {
            HandleIdleState();
        }
        else if(currentState == EnemyState.Chase)
        {
            HandleChaseState();
        }
        else if(currentState == EnemyState.Attack)
        {
            HandleAttackState();
        }
        else
        {
            HandleDeadState();
        }
    }

    void ResumeAgentSafely()
    {
        agent.isStopped = false;
    }

    void EnterState(EnemyState state)
    {
        if(state == EnemyState.Idle)
        {
            StopAgentSafely();
            IsChasing = false;
            IsAttacking = false;
        }
        else if(state == EnemyState.Chase)
        {
            ResumeAgentSafely();
            IsChasing = true;
            IsAttacking = false;
        }
        else if(state == EnemyState.Attack)
        {
            StopForAttack();
            IsChasing = false;
            IsAttacking = true;
        }
        else
        {
            StopAgentSafely();
            IsChasing = false;
            IsAttacking = false;
        }
    }

    void ExitState(EnemyState state)
    {
        // 아직은 처리할 것이 없음.
    }

    void ChangeState(EnemyState nextState)
    {
        if(currentState == nextState)
        {
            return;
        }

        ExitState(currentState);
        currentState = nextState;
        EnterState(currentState);
    }

    bool IsEnemyDead()
    {
        if(enemyHealth == null)
        {
            return false;
        }

        return enemyHealth.IsDead();
    }

    void UpdateStateByRules()
    {
        if(IsEnemyDead() == true)
        {
            ChangeState(EnemyState.Dead);
            return;
        }

        if(CanChaseTarget() == false)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        if(IsTargetInAttackRange() == true)
        {
            ChangeState(EnemyState.Attack);
            return;
        }

        ChangeState(EnemyState.Chase);
    }
}
