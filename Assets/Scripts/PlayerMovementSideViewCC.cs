using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
/*Jos ei ole vaadittavaa Componenttia asennettu GameObjectiin,
tällä saat luotua sen automaattisesti*/

public class PlayerMovementSideViewCC : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runSpeedMultiplier;

    [Header("Jumping")]
    [SerializeField] private bool jump;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMaxTime;
    [SerializeField] private bool isOnGround;

    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private bool isFalling;

    private Vector2 currentMovement;

    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;
    private CharacterController characterController = null;

    private void Awake()
    {
        input = new PlayerControls();
        characterController = GetComponent<CharacterController>();

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
        if (characterController != null)
        {
            Vector2 horizontalMovement = new(moveVector.x, moveVector.y);

            currentMovement.x = horizontalMovement.x * walkSpeed;
            characterController.Move(currentMovement * Time.deltaTime);

            // Flip the player's sprite based on direction of movement
            if (currentMovement.x > 0)
            {
                transform.localScale = new(-1, 1, 1); // Face left
            }
            else if (currentMovement.x < 0)
            {
                transform.localScale = new(1, 1, 1); // Face right
            }
        }
    }
    private void Jump()
    {
        if (characterController != null && jump && jumpTime < jumpMaxTime )
        {
            currentMovement.y += jumpSpeed * Time.deltaTime;
            jumpTime += 1.0f * Time.deltaTime;

        }
        if(jumpTime >= jumpMaxTime)
        {
            //jump = false; // Reset the jump flag
            isFalling = true;   
        }
    }

    private void GravityControl()
    {
        if (isFalling)
        {
            currentMovement.y -= gravity  * Time.deltaTime;
        }
    }

    private void Update()
    {
        Move();
        Jump();
        GravityControl();
    }


}
