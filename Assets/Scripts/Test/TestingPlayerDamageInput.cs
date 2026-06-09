using UnityEngine;

public class TestingPlayerDamageInput : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int testDamage = 20;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) == true)
        {
            ApplyTestDamage();
        }
    }

    void ApplyTestDamage()
    {
        if(playerHealth == null)
        {
            return;
        }

        playerHealth.TakeDamage(testDamage);
    }
}
