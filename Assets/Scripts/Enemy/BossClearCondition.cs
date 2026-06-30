using UnityEngine;

public class BossClearCondition : MonoBehaviour
{
    [SerializeField] private EnemyHealth bossHealth;
    
    [SerializeField] private bool unlockCursorOnClear = true;

    [SerializeField] private float resultSceneDelay = 1.5f;

    [SerializeField] private GameManager gameManager;

    private bool isCleared;

    // Update is called once per frame
    void Update()
    {
        if(isCleared == true)
        {
            return;
        }

        if(bossHealth == null)
        {
            return;
        }

        if(bossHealth.IsDead() == true)
        {
            HandleClear();
        }
    }

    void HandleClear()
    {
        isCleared = true;

        if(gameManager != null)
        {
            gameManager.RequestClear("You Killed Boss And Cleared Mission.");
        }
    }
}
