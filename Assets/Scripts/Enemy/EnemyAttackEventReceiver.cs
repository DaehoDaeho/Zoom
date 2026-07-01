using UnityEngine;

public class EnemyAttackEventReceiver : MonoBehaviour
{
    [SerializeField] private PlayerHealth targetHealth;
    [SerializeField] private int attackDamage = 10;

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
}
