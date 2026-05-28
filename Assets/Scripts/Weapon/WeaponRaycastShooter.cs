using UnityEngine;

/// <summary>
/// FPS 무기의 Raycast 발사를 담당하는 역할.
/// </summary>
public class WeaponRaycastShooter : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private WeaponView weaponView;
    [SerializeField] private float maxDistance = 100.0f;
    [SerializeField] LayerMask hitLayerMask;
    [SerializeField] private float fireInterval = 0.15f;
    [SerializeField] private float debugDuration = 0.25f;

    private float lastFireTime = -999.0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) == true)
        {
            TryFire();
        }
    }

    void TryFire()
    {
        if(CanFire() == false)
        {
            return;
        }

        lastFireTime = Time.time;
        FireRaycast();
    }

    bool CanFire()
    {
        if(playerCamera == null)
        {
            Debug.LogWarning("Player Camera가 연결되지 않았습니다.");
            return false;
        }

        if(weaponView == null)
        {
            Debug.LogWarning("WeaponView가 연결되지 않았습니다.");
            return false;
        }

        float elapsedTime = Time.time - lastFireTime;
        if(elapsedTime < fireInterval)
        {
            return false;
        }

        return true;
    }

    void FireRaycast()
    {
        Transform cameraTransform = playerCamera.transform;

        Vector3 rayOrigin = cameraTransform.position;
        Vector3 rayDirection = cameraTransform.forward;

        bool isHit = Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo, maxDistance, hitLayerMask);

        DrawDebugRay(rayOrigin, rayDirection, isHit, hitInfo);

        if(isHit == true)
        {
            HandleHit(hitInfo);
        }
        else
        {
            HandleMiss(rayOrigin, rayDirection);
        }
    }

    void HandleHit(RaycastHit hitInfo)
    {
        string hitName = hitInfo.transform.name;
        Vector3 hitPoint = hitInfo.point;
        float hitDistance = hitInfo.distance;

        Debug.Log("Raycast Hit / Target: " + hitName + " / Point: " + hitPoint +
            " / Distance: " + hitDistance);
    }

    void HandleMiss(Vector3 rayOrigin, Vector3 rayDirection)
    {
        Vector3 endPoint = rayOrigin + (rayDirection * maxDistance);
        Debug.Log("Raycast Hit / End Point: " + endPoint);
    }

    void DrawDebugRay(Vector3 rayOrigin, Vector3 rayDirection, bool isHit, RaycastHit hitInfo)
    {
        Color debugColor = Color.cyan;
        float drawDistance = maxDistance;

        if(isHit == true)
        {
            debugColor = Color.green;
            drawDistance = hitInfo.distance;
        }

        Debug.DrawRay(rayOrigin, rayDirection * drawDistance, debugColor, debugDuration);
    }
}
