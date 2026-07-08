using UnityEngine;

/// <summary>
/// 적 캐릭터의 체력과 사망 처리를 담당하는 역할.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 0;

    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private float destroyDelay = 0.8f;

    [SerializeField] private EnemyRagdollController ragdollController;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Collider collider;

    private bool isDead = false;

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public float HealthRatio
    {
        get
        {
            if(maxHealth <= 0)
            {
                return 0.0f;
            }

            float ratio = (float)currentHealth / (float)maxHealth;
            return ratio;
        }
    }

    void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int damageAmount)
    {
        if(isDead == true)
        {
            return;
        }

        currentHealth -= damageAmount;

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(isDead == true)
        {
            return;
        }

        isDead = true;

        if(rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }

        if(collider != null)
        {
            collider.enabled = false;
        }

        if(ragdollController != null)
        {
            ragdollController.SetRagdollActive(true);
        }

        if(destroyOnDeath == true)
        {
            Destroy(gameObject, destroyDelay);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
