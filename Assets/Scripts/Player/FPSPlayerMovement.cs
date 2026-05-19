using UnityEngine;

/// <summary>
/// FPS 플레이어의 수평 이동을 담당하는 역할.
/// WASD 입력을 읽고 CharacterController로 이동.
/// </summary>
public class FPSPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    private CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterController == null)
        {
            return;
        }

        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 rightDirection = transform.right * horizontalInput;
        Vector3 forwardDirection = transform.forward * verticalInput;
        Vector3 moveDirection = rightDirection + forwardDirection;

        moveDirection.Normalize();

        Vector3 velocity = moveDirection * moveSpeed;
        Vector3 frameMovement = velocity * Time.deltaTime;

        characterController.Move(frameMovement);
    }
}
