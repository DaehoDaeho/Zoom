using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    public void LoadGameScene()
    {
        GameResultData.ResetResult();
        SceneManager.LoadScene(GameSceneNames.GameScene);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
