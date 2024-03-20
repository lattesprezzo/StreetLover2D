using UnityEngine;
[RequireComponent(typeof(CharacterController))] 
/*Jos ei ole vaadittavaa Componenttia asennettu GameObjectiin,
tällä saat luotua sen automaattisesti*/
public class PlayerMovementSimple : MonoBehaviour
{

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector2 movement;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        bool isRunning = (Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift));
        float speed = isRunning ? runSpeed : walkSpeed;

        movement = new Vector2(moveX * speed, moveY * speed);
    }

    private void FixedUpdate()
    {
        characterController.Move(movement * Time.fixedDeltaTime);
    }
}
