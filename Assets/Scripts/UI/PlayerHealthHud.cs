using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthHud : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthFillImage;

    // Update is called once per frame
    void Update()
    {
        RefreshHud();
    }

    void RefreshHud()
    {
        if(playerHealth == null)
        {
            return;
        }

        RefreshHealthText();
        RefreshHealthFillImage();
    }

    void RefreshHealthText()
    {
        if(healthText == null)
        {
            return;
        }

        healthText.text = "Hp " + playerHealth.CurrentHealth + " / " + playerHealth.MaxHealth;
    }

    void RefreshHealthFillImage()
    {
        if(healthFillImage == null)
        {
            return;
        }

        healthFillImage.fillAmount = playerHealth.HealthRate;
    }
}
