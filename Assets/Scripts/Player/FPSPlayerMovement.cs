using UnityEngine;

/// <summary>
/// FPS 플레이어의 수평 이동을 담당하는 역할.
/// WASD 입력을 읽고 CharacterController로 이동.
/// </summary>
public class FPSPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float gravity = -20.0f;
    [SerializeField] private float jumpHeight = 1.6f;

    [SerializeField] private float groundedStickVelocity = -2.0f;

    private CharacterController characterController;
    private float verticalVelocity;

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
        Vector3 horizontalDirection = rightDirection + forwardDirection;

        if(horizontalDirection.sqrMagnitude > 1.0f)
        {
            horizontalDirection.Normalize();
        }

        Vector3 horizontalVelocity = horizontalDirection * moveSpeed;

        bool isGrounded = characterController.isGrounded;

        if(isGrounded == true && verticalVelocity < 0.0f)
        {
            verticalVelocity = groundedStickVelocity;
        }

        bool jumpInput = Input.GetKeyDown(KeyCode.Space);

        if(isGrounded == true && jumpInput == true)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 verticalMovement = Vector3.up * verticalVelocity;
        Vector3 totalVelocity = horizontalVelocity + verticalMovement;

        Vector3 frameMovement = totalVelocity * Time.deltaTime;

        characterController.Move(frameMovement);
    }
}
