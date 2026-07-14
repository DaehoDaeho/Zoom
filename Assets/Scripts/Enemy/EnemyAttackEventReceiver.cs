using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttackEventReceiver : MonoBehaviour
{
    [SerializeField] private PlayerHealth targetHealth;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask targetLayer;

    public void SetTargetHealth(PlayerHealth playerHealth)
    {
        targetHealth = playerHealth;
    }

    public void ApplyAttackDamage()
    {
        if(targetHealth == null)
        {
            return;
        }

        targetHealth.TakeDamage(attackDamage);
    }

    public void ApplyRaycast()
    {
        if(firePoint == null)
        {
            return;
        }

        bool isHit = Physics.Raycast(firePoint.position, transform.parent.forward, out RaycastHit hitInfo, 100.0f, targetLayer);

        if(isHit == true)
        {
            PlayerHealth playerHealth = hitInfo.collider.GetComponent<PlayerHealth>();

            if(playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("적이 원거리 공격으로 플레이어 명중!!!");
            }
        }
    }
}
