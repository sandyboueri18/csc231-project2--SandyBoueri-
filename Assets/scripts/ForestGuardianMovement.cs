using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class ForestGuardianMovement : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private InputActionAsset inputActions;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [Header("Magic Attack")]
    [SerializeField] private ParticleSystem guardianMagicAttack;

    [Header("Health")]
    [SerializeField] private int maximumHealth = 3;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField, Min(1)] private int maximumJumps = 2;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
  
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction; 

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private bool jumpRequested;
    private int jumpsRemaining = 1;
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
        jumpAction = playerActions.FindAction("Jump", true);
        moveAction = playerActions.FindAction("Move", true);
        attackAction = playerActions.FindAction("Attack", true);
    }


    private void OnEnable()
    {
        jumpAction?.Enable();
        moveAction?.Enable();
        attackAction?.Enable();
    }

    private void OnDisable()
    {
        jumpAction?.Disable();
        moveAction?.Disable();
        attackAction?.Disable();
        jumpRequested = false;
    }


    private void Update()
    {
        horizontalInput = isDead ? 0f : moveAction.ReadValue<float>();
        bool isOnGround = isGrounded();

        if (!isDead && jumpAction.WasPressedThisFrame())
        {
            jumpRequested = true;
        }
        if (!isDead && attackAction.WasPressedThisFrame())
        {
            if (guardianMagicAttack != null)
            {
                guardianMagicAttack.Play();
            }
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

        float movement = isDead ? 0f : horizontalInput * moveSpeed;

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
