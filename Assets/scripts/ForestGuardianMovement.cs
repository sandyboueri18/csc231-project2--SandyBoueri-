using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class ForestGuardianMovement : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private InputActionAsset inputActions;
    [Header("Movement")]
    //[SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField, Min(1)] private int maximumJumps = 2;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (inputActions == null)
        {
            Debug.LogError("Assign ForestGuardianActions!");
            enabled = false;
            return;
        }

        InputActionMap playerActions =inputActions.FindActionMap("Player", true);
        moveAction = playerActions.FindAction("Move", true);
        jumpAction = playerActions.FindAction("Jump", true);
        runAction = playerActions.FindAction("Run", true);
    }

    private void OnEnable()
    {
        moveAction?.Enable();
        jumpAction?.Enable();
        runAction?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
        jumpAction?.Disable();
        runAction?.Disable();
    }

    private void Update()
    {
        horizontalInput = moveAction.ReadValue<float>();

        if (Mathf.Abs(horizontalInput) > 0.05f)
        {
            spriteRenderer.flipX = horizontalInput < 0f;
        }
    }

    private void FixedUpdate()
    {
        bool isRunning = runAction.IsPressed();

        float speed = isRunning ? runSpeed : walkSpeed;

        float movement = horizontalInput * speed;

        body.linearVelocity = new Vector2(movement, body.linearVelocity.y);
    }
}
