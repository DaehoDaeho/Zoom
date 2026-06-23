using System.Collections;
using UnityEngine;

/// <summary>
/// 무기의 탄약과 재장전을 관리하는 역할.
/// </summary>
public class WeaponAmmo : MonoBehaviour
{
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private int reserveAmmo = 90;
    [SerializeField] private float reloadDuration = 1.5f;
    [SerializeField] private bool fillMagazineOnStart = true;

    [SerializeField] private int currentAmmo;
    private bool isReloading;
    private Coroutine reloadCoroutine;

    public int CurrentAmmo
    {
        get { return currentAmmo; }
    }

    public int MagazineSize
    {
        get { return magazineSize; }
    }

    public int ReserveAmmo
    {
        get { return reserveAmmo; }
    }

    public bool IsReloading
    {
        get { return isReloading; }
    }

    public bool HasAmmo
    {
        get { return currentAmmo > 0; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeAmmo();
    }

    public void InitializeAmmo()
    {
        magazineSize = Mathf.Max(1, magazineSize);
        reserveAmmo = Mathf.Max(0, reserveAmmo);

        if(fillMagazineOnStart == true)
        {
            currentAmmo = magazineSize;
        }
        else
        {
            currentAmmo = Mathf.Clamp(currentAmmo, 0, magazineSize);
        }
    }

    public bool CanShoot()
    {
        if(isReloading == true)
        {
            return false;
        }

        if(currentAmmo <= 0)
        {
            return false;
        }

        return true;
    }

    public bool TryConsumeAmmo()
    {
        if(CanShoot() == false)
        {
            return false;
        }

        --currentAmmo;
        return true;
    }

    public void TryStartReload()
    {
        if(CanReload() == false)
        {
            return;
        }

        reloadCoroutine = StartCoroutine(ReloadRoutine());
    }

    public bool CanReload()
    {
        if(isReloading == true)
        {
            return false;
        }

        if(currentAmmo >= magazineSize)
        {
            return false;
        }

        if(reserveAmmo <= 0)
        {
            return false;
        }

        return true;
    }

    IEnumerator ReloadRoutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadDuration);

        int neededAmmo = magazineSize - currentAmmo;
        int ammoToMove = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToMove;
        reserveAmmo -= ammoToMove;

        isReloading = false;
        reloadCoroutine = null;
    }

    public void AddReserveAmmo(int amount)
    {
        reserveAmmo += amount;
    }

    public void ApplyWeaponData(WeaponData weaponData)
    {
        magazineSize = weaponData.MagazineSize;
        reserveAmmo = weaponData.StartReserveAmmo;
        reloadDuration = weaponData.ReloadDuration;

        currentAmmo = magazineSize;
        isReloading = false;
    }
}
