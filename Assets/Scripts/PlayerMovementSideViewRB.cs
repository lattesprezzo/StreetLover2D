using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementSideViewRB : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runSpeedMultiplier;

    [Header("Jumping")]
    [SerializeField] private bool jump;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMaxTime;
    [SerializeField] private bool isOnGround;
    [SerializeField] private LayerMask groundLayer; // Set this to the layer of the ground in the inspector
    [SerializeField] private Transform groundCheck; // Set this to a point where the ground check should be performed
    [SerializeField] private float groundCheckRadius; // The radius of the ground check

    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private bool isFalling;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;

    Rigidbody2D rb;

    private Vector2 currentMovement;
    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;

    private void Awake()
    {
        input = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;
        input.Player.Jump.performed += OnJumpPerformed;
        input.Player.Jump.canceled += OnJumpCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;
        input.Player.Jump.performed -= OnJumpPerformed;
        input.Player.Jump.canceled -= OnJumpCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jump = true;
    }
    private void OnJumpCancelled(InputAction.CallbackContext context)
    {
        jump = false;
    }

    public void Move()
    {
        if (rb != null)
        {
            Vector2 horizontalMovement = new(moveVector.x, 0);

            // Apply the force for movement
            rb.velocity = horizontalMovement * walkSpeed;

            // Flip the player's sprite based on direction of movement
            if (rb.velocity.x > 0)
            {
                transform.localScale = new(-1, 1, 1); // Face right
            }
            else if (rb.velocity.x < 0)
            {
                transform.localScale = new(1, 1, 1); // Face left
            }
        }
    }
    private void Jump()
    {
        if (rb != null && jump && isOnGround)
        {
            //Vector2 jumpVelocity = new(0f, jumpForce);
            //rb.velocity += jumpVelocity;
            //Vector2 vel = rb.velocity;
            //vel.y += Physics2D.gravity.y;
           rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // Add an instantaneous upward force for the jump
            //  currentMovement.y += Physics2D.gravity.y * Time.deltaTime;
            //  rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            // jump = false; // Reset the jump flag
            if (rb.velocity.y >= 0)
            {
                rb.gravityScale = gravityScale;
            }
            else if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallingGravityScale;
            }
        }
    }


    private void GravityControl()
    {
        if (isFalling)
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }



    private void Update()
    {
        Move();
        Jump();
        // GravityControl();
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }


}
