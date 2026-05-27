using UnityEngine;

public class WeaponSetupTester : MonoBehaviour
{
    [SerializeField] private WeaponView weaponView;
    [SerializeField] private KeyCode debugFireKey = KeyCode.Mouse0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(debugFireKey) == true)
        {
            PrintFirePointDebugMessage();
        }
    }

    void PrintFirePointDebugMessage()
    {
        if(weaponView == null)
        {
            Debug.LogWarning("WeaponView가 연결되지 않았습니다.");
            return;
        }

        Transform firePoint = weaponView.FirePoint;
        if(firePoint == null)
        {
            Debug.LogWarning("FirePoint가 연결되지 않았습니다.");
            return;
        }

        Vector3 firePosition = firePoint.position;
        Vector3 fireDirection = firePoint.forward;

        Debug.Log("무기 입력 확인 / 위치 : " + firePosition + " / 방향 : " + fireDirection);
    }
}
