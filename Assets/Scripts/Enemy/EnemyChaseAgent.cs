using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseAgent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private PlayerHealth targetHealth;

    [SerializeField] private float targetRefreshInterval = 0.15f;

    [SerializeField] private float minTargetMoveDistance = 0.1f;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackRotateSpeed = 8.0f;

    private float attackTimer;
    public bool IsAttacking { get; private set; }

    private Vector3 lastTargetPosition;
    private float refreshTimer;

    public bool IsChasing { get; private set; }

    private void Awake()
    {
        lastTargetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanChaseTarget() == false)
        {
            StopAgentSafely();
            IsChasing = false;
            IsAttacking = false;
            return;
        }

        UpdateAttackTimer();

        if(IsTargetInAttackRange() == true)
        {
            StopForAttack();
            RotateToTarget();
            TryAttack();
            IsChasing = false;
            IsAttacking = true;
            return;
        }

        IsAttacking = false;
        refreshTimer += Time.deltaTime;
        if(refreshTimer >= targetRefreshInterval)
        {
            refreshTimer = 0.0f;
            UpdateDestinationIfNeeded();
        }
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

        if(targetHealth.IsDead == true)
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

        targetHealth.TakeDamage(attackDamage);
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
}
