using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class ForestGuardianMovement : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private InputActionAsset inputActions;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    
    [Header("Health")]
    [SerializeField] private int maximumHealth = 3;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;
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
    private bool jumpRequested;
    private int jumpsRemaining = 1;
    private bool isRunning;
    private bool isDead;
    private int currentHealth;


    public bool IsDead => isDead;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maximumHealth;

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
        jumpRequested = false;
    }


    private void Update()
    {
        horizontalInput = isDead ? 0f : moveAction.ReadValue<float>();
        bool isOnGround = isGrounded();
        isRunning = !isDead && runAction.IsPressed() && Mathf.Abs(horizontalInput) > 0.05f;

        if (!isDead && jumpAction.WasPressedThisFrame())
        {
            jumpRequested = true;
        }

        bool isFalling =!isDead && !isOnGround && body.linearVelocity.y < -0.1f;

        animator.SetFloat("speed", Mathf.Abs(horizontalInput));
        animator.SetBool("isGrounded", isOnGround);
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isDead", isDead);

       if (!isDead && Mathf.Abs(horizontalInput) > 0.05f)
        {
            spriteRenderer.flipX = horizontalInput < 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead || damage <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (currentHealth <= 0)
        {
            isDead = true;
            horizontalInput = 0f;
            jumpRequested = false;

            body.linearVelocity = Vector2.zero;

            animator.SetBool("isFalling", false);
            animator.SetBool("isDead", true);
        }

        Debug.Log(currentHealth);
    }

    public void Revive()
    {
        currentHealth = maximumHealth;
        isDead = false;
        jumpsRemaining = 1;

        animator.SetBool("isDead", false);
    }
    private void FixedUpdate()
    {
        if (isGrounded())
        {
            jumpsRemaining = maximumJumps;
        }
        else if (jumpsRemaining >= maximumJumps)
        {
            jumpsRemaining = maximumJumps - 1;
        }

        float speed = isRunning ? runSpeed : walkSpeed;

        float movement = isDead ? 0f : horizontalInput * speed;

        body.linearVelocity =
            new Vector2(movement, body.linearVelocity.y);

        if (jumpRequested)
        {
            if (!isDead && jumpsRemaining > 0)
            {
                body.linearVelocity =
                    new Vector2(body.linearVelocity.x, jumpForce);

                jumpsRemaining--;
            }

            jumpRequested = false;
        }
    }

    private bool isGrounded()
    {
        if (body != null && body.linearVelocity.y > 0f)
        {
            return false;
        }

        if (groundCheck == null)
        {
            return false;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(groundCheck.position,groundCheckRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ground") && !hit.isTrigger && hit.attachedRigidbody != body)
            {
                return true;
            }
        }

        return false;
    }


}
