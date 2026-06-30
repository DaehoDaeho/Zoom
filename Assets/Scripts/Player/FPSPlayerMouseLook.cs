using UnityEngine;

/// <summary>
/// FPS 플레이어의 마우스 시점 회전을 담당.
/// </summary>
public class FPSPlayerMouseLook : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float minPitch = -80.0f;
    [SerializeField] private float maxPitch = 80.0f;
    [SerializeField] private GameManager gameManager;

    private float cameraPitch = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(cameraTransform == null)
        {
            Camera mainCamera = Camera.main;
            if(mainCamera != null)
            {
                cameraTransform = mainCamera.transform;
            }
        }

        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraTransform != null)
        {
            LookAround();
        }
    }

    void LookAround()
    {
        if (gameManager != null && gameManager.CurrentState != GameState.Playing)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float yawAmount = mouseX * mouseSensitivity;
        transform.Rotate(0.0f, yawAmount, 0.0f);

        float pitchAmount = mouseY * mouseSensitivity;
        cameraPitch -= pitchAmount;
        cameraPitch = Mathf.Clamp(cameraPitch, minPitch, maxPitch);

        // Quaternion.Euler : 각도를 이용해서 오브젝트를 회전시켜주는 함수.
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0.0f, 0.0f);

        //if(Input.GetKeyDown(KeyCode.Escape) == true)
        //{
        //    UnlockCursor();
        //}

        //if(Input.GetMouseButtonDown(0) == true)
        //{
        //    LockCursor();
        //}
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
