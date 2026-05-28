using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform weaponRoot;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float previewDistance = 3.0f;

    public Transform FirePoint
    {
        get { return firePoint; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ValidateReferences();
    }

    // Update is called once per frame
    void Update()
    {
        DrawFirePointPreview();
    }

    /// <summary>
    /// Inspector에서 연결해야 하는 무기 참조들이 비어 있는지 검사.
    /// </summary>
    void ValidateReferences()
    {
        if(weaponHolder == null)
        {
            Debug.LogWarning("weaponHolder가 연결되지 않았습니다.");
        }

        if (weaponRoot == null)
        {
            Debug.LogWarning("weaponRoot가 연결되지 않았습니다.");
        }

        if (firePoint == null)
        {
            Debug.LogWarning("firePoint가 연결되지 않았습니다.");
        }
    }

    void DrawFirePointPreview()
    {
        //if(firePoint == null)
        //{
        //    return;
        //}

        //Vector3 previewStart = firePoint.position;
        //Vector3 previewDirection = firePoint.forward;
        //Vector3 previewEnd = previewDirection * previewDistance;

        //Debug.DrawRay(previewStart, previewEnd, Color.cyan);
    }
}
