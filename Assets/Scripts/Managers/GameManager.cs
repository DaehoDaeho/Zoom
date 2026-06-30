using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private float resultSceneDelay = 1.5f;

    private bool isEnding;

    public GameState CurrentState { get; private set; } = GameState.Playing;

    public bool IsPlaying
    {
        get { return CurrentState == GameState.Playing; }
    }

    private void Awake()
    {
        Time.timeScale = 1.0f;
        ChangeState(GameState.Playing);
        SetPausePanel(false);
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            TogglePause();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SetPausePanel(bool active)
    {
        if(pausePanel != null)
        {
            pausePanel.SetActive(active);
        }
    }

    public void LoadTitleScene()
    {
        Time.timeScale = 1.0f;
        GameResultData.ResetResult();
        SceneManager.LoadScene(GameSceneNames.TitleScene);
    }

    public void LoadResultScene()
    {
        SceneManager.LoadScene(GameSceneNames.ResultScene);
    }

    public void RequestGameOver(string message)
    {
        if(isEnding == true)
        {
            return;
        }

        isEnding = true;
        ChangeState(GameState.GameOver);
        Time.timeScale = 1.0f;
        SetPausePanel(false);

        GameResultData.SetGameOverResult(message);
        Invoke("LoadResultScene", resultSceneDelay);
    }

    public void RequestClear(string message)
    {
        if(isEnding == true)
        {
            return;
        }

        isEnding = true;
        ChangeState(GameState.Clear);
        Time.timeScale = 1.0f;
        SetPausePanel(false);
        GameResultData.SetClearResult(message);
        Invoke("LoadResultScene", resultSceneDelay);
    }

    public void ResumeGame()
    {
        if(CurrentState != GameState.Paused)
        {
            return;
        }

        ChangeState(GameState.Playing);
        Time.timeScale = 1.0f;
        SetPausePanel(false);
        LockCursor();
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        ChangeState(GameState.Paused);
        Time.timeScale = 0.0f;
        SetPausePanel(true);
        UnlockCursor();
    }

    public void TogglePause()
    {
        if(CurrentState == GameState.Playing)
        {
            PauseGame();
            return;
        }

        if (CurrentState == GameState.Paused)
        {
            ResumeGame();
        }
    }

    void ChangeState(GameState nextState)
    {
        CurrentState = nextState;
    }
}
