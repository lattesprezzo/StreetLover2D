using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runSpeedMultiplier;
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
        if (characterController != null)
        {
            Vector2 horizontalMovement = new(moveVector.x, moveVector.y);

            currentMovement.x = horizontalMovement.x  * walkSpeed;
            currentMovement.y = horizontalMovement.y * walkSpeed;
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }


    private void Update()
    {
        Move();
    }


}
