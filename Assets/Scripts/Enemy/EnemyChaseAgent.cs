using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseAgent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private PlayerHealth targetHealth;

    [SerializeField] private float targetRefreshInterval = 0.15f;

    [SerializeField] private float minTargetMoveDistance = 0.1f;

    [SerializeField] private NavMeshAgent agent;

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
            return;
        }

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
}
