using TMPro;
using UnityEngine;

/// <summary>
/// 탄약 상태를 화면 HUD에 표시하는 역할.
/// </summary>
public class WeaponAmmoHud : MonoBehaviour
{
    [SerializeField] private WeaponAmmo weaponAmmo;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text reloadText;

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
}
