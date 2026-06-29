using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultSceneController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultTitleText;
    [SerializeField] private TMP_Text resultMessageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshResultText();
        UnlockCursor();
    }

    void RefreshResultText()
    {
        if(resultTitleText != null)
        {
            resultTitleText.text = GameResultData.ResultTitle;
        }

        if(resultMessageText != null)
        {
            resultMessageText.text = GameResultData.ResultMessage;
        }
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadGameScene()
    {
        GameResultData.ResetResult();
        SceneManager.LoadScene(GameSceneNames.GameScene);
    }

    public void LoadTitleScene()
    {
        GameResultData.ResetResult();
        SceneManager.LoadScene(GameSceneNames.TitleScene);
    }
}
