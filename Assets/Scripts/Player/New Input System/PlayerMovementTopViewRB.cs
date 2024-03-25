using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
/*Jos ei ole vaadittavaa Componenttia asennettu GameObjectiin,
tällä saat luotua sen automaattisesti*/

public class PlayerMovementTopViewRB : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runSpeedMultiplier;
    private Vector2 currentMovement;

    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;

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
    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

    }
    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
    }

    public void Move()
    {
        if (rb != null)
        {
            Vector2 horizontalMovement = new(moveVector.x, moveVector.y);

            currentMovement.x = horizontalMovement.x * walkSpeed;
            currentMovement.y = horizontalMovement.y * walkSpeed;
            rb.velocity = new(currentMovement.x, currentMovement.y); 


        }
    }

    private void Update()
    {
        Move();
    }


}
