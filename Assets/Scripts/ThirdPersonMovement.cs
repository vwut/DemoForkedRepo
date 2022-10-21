using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float gravity;
    public float jumpStrength;
    Vector3 playerVelocity;

    public InputActionReference MovementControl;
    public InputActionReference JumpControl;
    public InputActionReference SprintControl;

    private Transform cameraTransform;
    private Transform referenceTransform;
    public Transform playerBody;


    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;


    public GameObject combatCamera;
    private bool isGrounded;

    [SerializeField]
    public float rotationSpeed;

    void Start() {
        referenceTransform = new GameObject().transform;
    }

    void FixedUpdate() {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -1.5f;
        }

        referenceTransform.eulerAngles = new Vector3(0, cameraTransform.eulerAngles.y, 0);
        Vector3 direction = MovementControl.action.ReadValue<Vector2>();
        direction = referenceTransform.forward* direction.y + referenceTransform.right * direction.x;
        direction.y = 0f;

        Vector3 combatDirection = referenceTransform.forward;
        combatDirection.y = 0f;

        
        if (direction != Vector3.zero)
        {
            playerBody.forward = Vector3.Slerp(playerBody.forward, direction.normalized, Time.deltaTime * rotationSpeed);
        }
        if (JumpControl.action.WasPressedThisFrame() && isGrounded) {
            playerVelocity.y += Mathf.Sqrt(jumpStrength * -3.0f * gravity);
        }

        if (SprintControl.action.IsPressed()) {
            speed = 15f;
        } else {
            speed = 10f;
        }

        if (combatCamera.activeInHierarchy) {
            playerBody.forward = combatDirection;
        }

        controller.Move(direction * speed * Time.deltaTime);
        playerVelocity.y += gravity * 1.1f * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }
}
