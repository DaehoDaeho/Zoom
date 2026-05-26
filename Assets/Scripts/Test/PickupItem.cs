using UnityEngine;

public class PickupItem : MonoBehaviour
{
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

        Debug.Log("Item Picked Up : " + gameObject.name);

        gameObject.SetActive(false);
    }
}
