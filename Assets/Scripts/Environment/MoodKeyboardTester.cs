using UnityEngine;

public class MoodKeyboardTester : MonoBehaviour
{
    [SerializeField] private SceneMoodController moodController;

    // Update is called once per frame
    void Update()
    {
        if(moodController == null)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            moodController.ApplyMood(SceneMoodType.Normal);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {
            moodController.ApplyMood(SceneMoodType.Combat);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {
            moodController.ApplyMood(SceneMoodType.Danger);
        }
    }
}
