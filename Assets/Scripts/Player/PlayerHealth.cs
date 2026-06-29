using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private bool isDead;

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    public float HealthRate
    {
        get
        {
            if(maxHealth <= 0)
            {
                return 0.0f;
            }

            return (float)currentHealth / maxHealth;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialHealth();
    }

    public void InitialHealth()
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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
        currentHealth = 0;

        HandleDeath();
    }

    void HandleDeath()
    {
        GameResultData.SetGameOverResult("You Are Dead. Try Again.");

        SceneManager.LoadScene(GameSceneNames.ResultScene);
    }

    public bool Heal(int healAmount)
    {
        if(isDead == true)
        {
            return false;
        }

        if(currentHealth >= maxHealth)
        {
            return false;
        }

        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        return true;
    }
}
