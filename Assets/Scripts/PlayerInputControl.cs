using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;
    [Header("Input Map")]
  //  [SerializeField] private string actionMapName = "Player";
    [Header("Actions")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string run = "Run";
    [SerializeField] private string jump = "Jump";

    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;

    public Vector2 MoveInput { get; private set; }
    public float RunValue { get; private set; }
    public bool RunInput { get; private set; }

    public static PlayerInput Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
      // moveAction = playerControls.actionMaps
    }

    private void OnEnable()
    {
        moveAction.Enable();
        runAction.Enable();
        jumpAction.Enable();    
        
    }
    private void OnDisable()
    {
        moveAction?.Disable();  
        runAction?.Disable();   
        jumpAction?.Disable();  
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
