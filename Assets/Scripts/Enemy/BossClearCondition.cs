using UnityEngine;
using UnityEngine.SceneManagement;

public class BossClearCondition : MonoBehaviour
{
    [SerializeField] private EnemyHealth bossHealth;
    
    [SerializeField] private bool unlockCursorOnClear = true;

    [SerializeField] private float resultSceneDelay = 1.5f;

    private bool isCleared;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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

        GameResultData.SetClearResult("You Killed Boss And Cleared Mission.");

        Invoke("LoadResultScene", resultSceneDelay);
    }

    void LoadResultScene()
    {
        SceneManager.LoadScene(GameSceneNames.ResultScene);
    }
}
