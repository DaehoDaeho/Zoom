using UnityEngine;

/// <summary>
/// FPS 플레이어의 기본 참조 구조를 확인하는 역할.
/// </summary>
public class FPSPlayerReferences : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }

        ValidateReferences();
    }

    void ValidateReferences()
    {
        if(characterController == null)
        {
            Debug.LogError("FPSPlayer에 CharacterController가 없습니다.", this);
        }

        if(playerCamera == null)
        {
            Debug.LogError("FPSPlayer 자식에 Camera가 없습니다.", this);
        }
    }
}
