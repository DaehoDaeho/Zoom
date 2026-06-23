using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 탄약 상태를 화면 HUD에 표시하는 역할.
/// </summary>
public class WeaponAmmoHud : MonoBehaviour
{
    [SerializeField] private WeaponAmmo weaponAmmo;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text reloadText;
    [SerializeField] private Image[] crosshairImages;
    [SerializeField] private Color warningCrosshairColor = Color.red;

    [SerializeField] private int lowAmmoThreshold = 5;

    private Color originalCrosshairColor;

    private void Awake()
    {
        if(crosshairImages != null)
        {
            originalCrosshairColor = crosshairImages[0].color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RefreshHud();
    }

    void RefreshHud()
    {
        if(weaponAmmo == null)
        {
            CleanHud();
            return;
        }

        RefreshAmmoText();
        RefreshReloadText();
        RefreshCrosshairColor();
    }

    void RefreshAmmoText()
    {
        if(weaponAmmo == null)
        {
            return;
        }

        ammoText.text = weaponAmmo.CurrentAmmo + " / " + weaponAmmo.MagazineSize +
            "  Reserve " + weaponAmmo.ReserveAmmo;
    }

    void RefreshReloadText()
    {
        if(weaponAmmo.IsReloading == true)
        {
            reloadText.text = "RELOADING...";
        }
        else
        {
            reloadText.text = string.Empty;
        }
    }

    void RefreshCrosshairColor()
    {
        if(weaponAmmo.CurrentAmmo <= lowAmmoThreshold)
        {
            SetCrosshairColor(warningCrosshairColor);
        }
        else
        {
            SetCrosshairColor(originalCrosshairColor);
        }
    }

    void SetCrosshairColor(Color color)
    {
        if(crosshairImages != null)
        {
            for(int i=0; i<crosshairImages.Length; ++i)
            {
                crosshairImages[i].color = color;
            }
        }
    }

    void CleanHud()
    {
        if(ammoText != null)
        {
            ammoText.text = "-- / --   Reserve --";
        }

        if(reloadText != null)
        {
            reloadText.text = string.Empty;
        }
    }

    public void SetWeaponAmmo(WeaponAmmo newWeaponAmmo)
    {
        weaponAmmo = newWeaponAmmo;
        RefreshHud();
    }
}
