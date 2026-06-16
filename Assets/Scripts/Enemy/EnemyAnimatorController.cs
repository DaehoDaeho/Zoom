using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyChaseAgent enemyAgent;
    [SerializeField] private NavMeshAgent navMeshAgent;

    // 이동 애니메이션 전환 보간 시간.
    [SerializeField] private float moveSpeedDampTime = 0.1f;

    [SerializeField] private string moveSpeedParam = "moveSpeed";
    [SerializeField] private string isAttackingParam = "isAttacking";
    [SerializeField] private string isDeadParam = "isDead";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveSpeed();
        UpdateAttackState();
        UpdateDeadState();
    }

    void UpdateMoveSpeed()
    {
        float moveSpeed = 0.0f;

        if(enemyAgent.CurrentState == EnemyState.Chase)
        {
            moveSpeed = navMeshAgent.velocity.magnitude;
        }

        animator.SetFloat(moveSpeedParam, moveSpeed, moveSpeedDampTime, Time.deltaTime);
    }

    void UpdateAttackState()
    {
        bool isAttacking = enemyAgent.CurrentState == EnemyState.Attack;
        animator.SetBool(isAttackingParam, isAttacking);
    }

    void UpdateDeadState()
    {
        bool isDead = enemyAgent.CurrentState == EnemyState.Dead;
        animator.SetBool(isDeadParam, isDead);
    }
}
