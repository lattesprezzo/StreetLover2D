using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runSpeedMultiplier;

    [Header("Jumping")]
    [SerializeField] private bool jump;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime = 0;
    [SerializeField] private float jumpTimeMultiplier;
    [SerializeField] private float jumpMaxTime;
 //[SerializeField] private bool IsOnGround;
    [SerializeField] private LayerMask groundLayer; // Set this to the layer of the ground in the inspector
    [SerializeField] private Transform groundCheck; // Set this to a point where the ground check should be performed
    [SerializeField] private float groundCheckRadius; // The radius of the ground check

    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private bool isFalling;
    public float gravityScale;
    public float fallingGravityScale;
    public float gravityScaleLimit;
    public float fallingGravityScaleLimit;
    Rigidbody2D rb;

    private Vector2 currentMovement;
    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;

    [Header("Shooting")]
    public GameObject loveBullet; // The bullet prefab
    public float bulletSpeed = 10f; // The speed of the bullet
    public bool Shooting; // Whether the player can shoot
    private int direction = 1; // 1 for right, -1 for left
    public Transform bulletSpawnLocation;
    public float bulletLifeTime;

    private void Awake()
    {
        input = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        //  loveBullet = Instantiate(loveBullet, bulletSpawnLocation.position, Quaternion.identity);
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCancelled;
        input.Player.Jump.performed += OnJumpPerformed;
        input.Player.Jump.canceled += OnJumpCancelled;
        input.Player.Shoot.performed += OnShootPerformed;
        input.Player.Shoot.canceled += OnShootCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;
        input.Player.Jump.performed -= OnJumpPerformed;
        input.Player.Jump.canceled -= OnJumpCancelled;
        input.Player.Shoot.performed -= OnShootPerformed;
        input.Player.Shoot.canceled -= OnShootCancelled;

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
    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        Shooting = true;
    }
    private void OnShootCancelled(InputAction.CallbackContext context)
    {
        Shooting = false;
    }

    public void Move()
    {
        if (rb != null)
        {
            // Flip the player's sprite based on direction of movement
            if (rb.linearVelocity.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Face right
                direction = 1;

            }
            else if (rb.linearVelocity.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Face left
                direction = -1;
            }
        }
    }
    bool IsOnGround()
    {
        // isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        return Physics2D.OverlapCapsule(groundCheck.position, new(1f, 2f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    private void Jump()
    {
        if (rb != null && jump && IsOnGround() && jumpTime == 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        jumpTime += jumpTimeMultiplier * Time.deltaTime;

        if (jumpTime == jumpMaxTime / 2 || IsOnGround())
        {
            jumpTime = 0;
        }


    }
    public void Shoot()
    {
        if (Shooting)
        {
            // Create a new bullet at the player's position
            GameObject bullet = Instantiate(loveBullet, bulletSpawnLocation.position, Quaternion.identity);

            // Get the bullet's Rigidbody2D component
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

            // Set the bullet's velocity based on the player's facing direction
            rbBullet.linearVelocity = new Vector2(bulletSpeed * direction, 0);

            // Remember to destroy or hide the prefab!
            bulletLifeTime -= Time.deltaTime;

            if (bulletLifeTime <= 0)
            {
                // This destroys the bullet from memory (Heavy for performance)
                Destroy(bullet);

                // This hides the 
            }
        }
    }

    private void Update()
    {
        Move();
        Jump();
        Shoot();

        //1. keino liikuttaa hahmoa: Liikutetaan Rigidbodya. y-akseli ei ota input-arvoa t�ss�
        // vaan sen hoitaa Jump()-funktio erikseen. "rb.velocity.y" vain p�ivitt�� y-arvoa liikkumisen mukana.
        currentMovement = new(moveVector.x * walkSpeed, rb.linearVelocity.y);
        // Apply the force for movement
        rb.linearVelocity = currentMovement;

        // 2.keino liikuttaa hahmoa edestakaisin. Huomaa, ett� y-akselin hoitaa Jump()-funktio erikseen:
        // Translate on hieman herkempi ottamaan vastaan arvoja, joten walkSpeed tulisi olla paljon pienempi (0.005f).
        //transform.Translate(new(moveVector.x * walkSpeed, 0, 0));


    }
}
