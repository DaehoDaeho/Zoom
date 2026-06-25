using UnityEngine;

public class BossClearCondition : MonoBehaviour
{
    [SerializeField] private EnemyHealth bossHealth;
    [SerializeField] private GameObject clearPanel;

    [SerializeField] private bool unlockCursorOnClear = true;

    private bool isCleared;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(clearPanel != null)
        {
            clearPanel.SetActive(false);
        }
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

        if(clearPanel != null)
        {
            clearPanel.SetActive(true);
        }

        if(unlockCursorOnClear == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
