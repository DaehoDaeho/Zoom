using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private EnemyHealth bossHealth;
    [SerializeField] private string bossName = "Boss";
    [SerializeField] private GameObject bossPanel;
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text hpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateBossName();
        UpdateVisibleState();
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisibleState();
        UpdateHealthBar();
    }

    public void SetBoss(EnemyHealth newBossHealth)
    {
        bossHealth = newBossHealth;
        UpdateVisibleState();
        UpdateHealthBar();
    }

    void UpdateVisibleState()
    {
        if(bossPanel == null)
        {
            return;
        }

        bool shouldShow = bossHealth != null && bossHealth.IsDead() == false;
        bossPanel.SetActive(shouldShow);
    }

    void UpdateBossName()
    {
        if(nameText == null)
        {
            return;
        }

        nameText.text = bossName;
    }

    void UpdateHealthBar()
    {
        if(bossHealth == null)
        {
            return;
        }

        if(fillImage != null)
        {
            fillImage.fillAmount = bossHealth.HealthRatio;
        }

        if(hpText != null)
        {
            hpText.text = bossHealth.CurrentHealth + " / " + bossHealth.MaxHealth;
        }
    }
}
