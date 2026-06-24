using UnityEngine;

[System.Serializable]
public class WeaponSlot
{
    public string slotName;
    public GameObject weaponObject;
    public WeaponData weaponData;

    [HideInInspector] public WeaponRaycastShooter shooter;
    [HideInInspector] public WeaponProjectileShooter projectileShooter;
    [HideInInspector] public WeaponAmmo ammo;
}

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private WeaponSlot[] weaponSlots;
    [SerializeField] private WeaponAmmoHud weaponAmmoHud;

    private int currentWeaponIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConfigureWeaponSlots();
        SelectWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        HandleNumberKeyInput();
        HandleMouseWheelInput();
    }

    void ConfigureWeaponSlots()
    {
        for(int i=0; i<weaponSlots.Length; ++i)
        {
            ConfigureWeaponSlot(i);
        }
    }

    void ConfigureWeaponSlot(int index)
    {
        WeaponSlot slot = weaponSlots[index];

        if(slot == null || slot.weaponObject == null)
        {
            return;
        }

        slot.shooter = slot.weaponObject.GetComponentInChildren<WeaponRaycastShooter>();

        slot.projectileShooter = slot.weaponObject.GetComponentInChildren<WeaponProjectileShooter>();

        slot.ammo = slot.weaponObject.GetComponentInChildren<WeaponAmmo>();

        if(slot.shooter != null)
        {
            slot.shooter.ApplyWeaponData(slot.weaponData);
        }

        if(slot.projectileShooter != null)
        {
            slot.projectileShooter.ApplyWeaponData(slot.weaponData);
        }

        if(slot.ammo != null)
        {
            slot.ammo.ApplyWeaponData(slot.weaponData);
        }

        slot.weaponObject.SetActive(false);
    }

    void HandleNumberKeyInput()
    {
        for(int i=0; i<weaponSlots.Length; ++i)
        {
            KeyCode keyCode = KeyCode.Alpha1 + i;
            if(Input.GetKeyDown(keyCode) == true)
            {
                SelectWeapon(i);
            }
        }
    }

    void HandleMouseWheelInput()
    {
        float wheelValue = Input.mouseScrollDelta.y;
        if(wheelValue > 0.0f)
        {
            SelectNextWeapon();
        }
        else if(wheelValue < 0.0f)
        {
            SelectPreviousWeapon();
        }
    }

    void SelectNextWeapon()
    {
        int previousIndex = currentWeaponIndex + 1;
        if (previousIndex >= weaponSlots.Length)
        {
            previousIndex = 0;
        }

        SelectWeapon(previousIndex);
    }

    void SelectPreviousWeapon()
    {
        int previousIndex = currentWeaponIndex - 1;
        if(previousIndex < 0)
        {
            previousIndex = weaponSlots.Length - 1;
        }

        SelectWeapon(previousIndex);
    }

    public void SelectWeapon(int index)
    {
        DeactiveCurrentWeapon();
        currentWeaponIndex = index;
        ActiveCurrentWeapon();
        RefreshWeaponHud();
    }

    void DeactiveCurrentWeapon()
    {
        if(currentWeaponIndex < 0 || currentWeaponIndex >= weaponSlots.Length)
        {
            return;
        }

        weaponSlots[currentWeaponIndex].weaponObject.SetActive(false);
    }

    void ActiveCurrentWeapon()
    {
        if (currentWeaponIndex < 0 || currentWeaponIndex >= weaponSlots.Length)
        {
            return;
        }

        weaponSlots[currentWeaponIndex].weaponObject.SetActive(true);
    }

    void RefreshWeaponHud()
    {
        if(weaponAmmoHud == null)
        {
            return;
        }

        WeaponAmmo currentAmmo = weaponSlots[currentWeaponIndex].ammo;
        weaponAmmoHud.SetWeaponAmmo(currentAmmo);
    }
}
