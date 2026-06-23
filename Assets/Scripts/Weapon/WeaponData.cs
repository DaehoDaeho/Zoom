using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "FPS/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string weaponName = "New Weapon";

    [SerializeField] private int damage = 20;
    [SerializeField] private float fireInterval = 0.2f;
    [SerializeField] private float maxDistance = 100.0f;

    [SerializeField] private int magazineSize = 30;
    [SerializeField] private int startReserveAmmo = 90;
    [SerializeField] private float reloadDuration = 1.2f;

    public string WeaponName
    {
        get { return weaponName; }
    }

    public int Damage
    {
        get { return damage; }
    }

    public float FireInterval
    {
        get { return fireInterval; }
    }

    public float MaxDistance
    {
        get { return maxDistance; }
    }

    public int MagazineSize
    {
        get { return magazineSize; }
    }

    public int StartReserveAmmo
    {
        get { return startReserveAmmo; }
    }

    public float ReloadDuration
    {
        get { return reloadDuration; }
    }
}
