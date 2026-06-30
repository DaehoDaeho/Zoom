using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponProjectileShooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float launchSpeed = 18.0f;
    [SerializeField] private float projectileLifeTime = 4.0f;
    [SerializeField] private float explosionRadius = 1.5f;
    [SerializeField] private WeaponAmmo weaponAmmo;
    [SerializeField] private WeaponFeedback weaponFeedback;
    [SerializeField] private WeaponData weaponData;

    private int damage = 20;
    private float fireInterval = 0.5f;
    private float nextFireTime;

    public void ApplyWeaponData(WeaponData newWeaponData)
    {
        weaponData = newWeaponData;
        damage = weaponData.Damage;
        fireInterval = weaponData.FireInterval;
    }

    void HandleReloadInput()
    {
        if(Input.GetKeyDown(KeyCode.R) == true)
        {
            if(weaponAmmo != null)
            {
                weaponAmmo.TryStartReload();
            }
        }
    }

    void HandleFireInput()
    {
        if(Input.GetMouseButtonDown(0) == true)
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                return;
            }

            TryFire();
        }
    }

    void TryFire()
    {
        if(CanFire() == false)
        {
            return;
        }

        if(weaponAmmo.TryConsumeAmmo() == false)
        {
            return;
        }

        nextFireTime = Time.time + fireInterval;

        if(weaponFeedback != null)
        {
            weaponFeedback.PlayFireFeedback();
        }

        SpawnProjectile();
    }

    bool CanFire()
    {
        if(weaponAmmo == null)
        {
            return false;
        }

        if(projectilePrefab == null || firePoint == null)
        {
            return false;
        }

        if(Time.time < nextFireTime)
        {
            return false;
        }

        if(weaponAmmo.CanShoot() == false)
        {
            return false;
        }

        return true;
    }

    void SpawnProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        if(projectile != null)
        {
            projectile.Initialize(damage, projectileLifeTime, explosionRadius);
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            if(projectileRigidbody != null)
            {
                projectileRigidbody.linearVelocity = firePoint.forward * launchSpeed;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(weaponData != null)
        {
            ApplyWeaponData(weaponData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleReloadInput();
        HandleFireInput();
    }
}
