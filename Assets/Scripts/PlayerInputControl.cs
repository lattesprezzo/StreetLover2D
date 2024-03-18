using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;
    [Header("Input Map")]
    [SerializeField] private string actionMapName = "Player";
    [Header("Actions")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string run = "Run";

    private InputAction moveAction;
    private InputAction runAction;  

    public Vector2 MoveInput { get; private set; }
    public float RunValue { get; private set; }
    public bool RunInput { get; private set; }

    public static PlayerInput Instance { get; private set; }    
    void Start()
    {

    }

    void Update()
    {

    }
}
