using UnityEngine;
using UnityEngine.AI;

public class EnemyRagdollController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;

    private void Awake()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        SetRagdollActive(false);
    }

    public void SetRagdollActive(bool active)
    {
        if(animator != null)
        {
            animator.enabled = !active;
        }

        if(navMeshAgent != null)
        {
            navMeshAgent.enabled = !active;
        }

        for(int i=0; i<ragdollRigidbodies.Length; ++i)
        {
            ragdollRigidbodies[i].isKinematic = !active;
            ragdollRigidbodies[i].useGravity = active;
        }

        for(int i=0; i<ragdollColliders.Length; ++i)
        {
            ragdollColliders[i].enabled = active;
        }
    }
}
