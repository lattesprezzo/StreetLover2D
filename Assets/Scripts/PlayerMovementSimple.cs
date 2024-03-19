using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour
{

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runSpeedMultiplier;

    private CharacterController characterController;
    private PlayerInputControl inputControl;
    private Vector2 currentMovement;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputControl = GetComponent<PlayerInputControl>();
    }

    private void ControlMovement()
    {
        if (characterController != null)
        {
            Vector2 horizontalMovement = new(inputControl.MoveInput.x, inputControl.MoveInput.y);
            float speed = walkSpeed * (inputControl.RunValue > 0 ? runSpeedMultiplier : 1f);
            currentMovement.x = horizontalMovement.x + speed;
            currentMovement.y = horizontalMovement.y + speed;
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }
    void Start()
    {

    }


    void Update()
    {
        ControlMovement();
    }
}
