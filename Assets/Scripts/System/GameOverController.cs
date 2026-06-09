using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject gameOverPanel;

    private bool isGameOverShown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameOverShown = false;
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOverShown == true)
        {
            return;
        }

        if(playerHealth == null)
        {
            return;
        }

        if(playerHealth.IsDead == true)
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        isGameOverShown = true;

        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
