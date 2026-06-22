using UnityEngine;

public enum PickupItemType
{
    Health,
    Ammo,
    HealthAndAmmo
}

public class PickupItem : MonoBehaviour
{
    [SerializeField] private PickupItemType itemType = PickupItemType.Health;
    [SerializeField] private int healAmount = 25;
    [SerializeField] private int reserveAmmoAmount = 30;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private AudioClip pickupSound;

    bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if(isPickedUp == true)
        {
            return;
        }

        if(other.CompareTag("Player") == false)
        {
            return;
        }

        isPickedUp = true;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if(playerHealth == null)
        {
            return;
        }

        WeaponAmmo weaponAmmo = other.GetComponentInChildren<WeaponAmmo>();

        if(weaponAmmo == null)
        {
            return;
        }

        bool applied = ApplyPickup(playerHealth, weaponAmmo);

        Debug.Log("Item Picked Up : " + gameObject.name);

        //gameObject.SetActive(false);
        ConsumeItem();
    }

    void ConsumeItem()
    {
        isPickedUp = true;

        PlayPickupFeedback();

        Destroy(gameObject);
    }

    void PlayPickupFeedback()
    {
        if(pickupEffect != null)
        {
            GameObject effectObject = Instantiate(pickupEffect, transform.position, Quaternion.identity);

            if(effectObject != null)
            {
                Destroy(effectObject, 1.5f);
            }
        }

        if(pickupSound != null)
        {
            // AudioSource.PlayClipAtPoint
            // 특정 위치에서 사운드를 재생하는 함수.
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
    }

    bool ApplyPickup(PlayerHealth playerHealth, WeaponAmmo weaponAmmo)
    {
        bool applied = false;

        if(itemType == PickupItemType.Health || itemType == PickupItemType.HealthAndAmmo)
        {
            applied = playerHealth.Heal(healAmount);
        }

        if(itemType == PickupItemType.Ammo || itemType == PickupItemType.HealthAndAmmo)
        {
            weaponAmmo.AddReserveAmmo(reserveAmmoAmount);
            applied = true;
        }

        return applied;
    }
}
