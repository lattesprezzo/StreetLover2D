using UnityEngine;

public class PlayerMovementSideWay : MonoBehaviour
{
    // Hidden in the inspector (Private variable). We don't need to see these, since we are only giving these a value by pressing a button.
    float leftmove = 0.0f;
    float rightmove = 0.0f;

    private int direction = 1; // 1 for right, -1 for left
    public float moveSpeed;
    public float jumpForce;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask jumpableObject;

    //--- AUDIO
    public AudioSource jumpsound;
    public AudioClip jumpClip;


    private void Awake()
    {
        //  jumpsound  = GetComponent<AudioSource>();
    }
    void Start()
    {


        //  DontDestroyOnLoad(gameObject);
    }


    bool IsGrounded()
    {
        // groundCheck.position antaa groundCheckin sijainnin. 4f on ympyrämitta, kuinka laajalta alueelta tarkistetaan
        // ground-alue ja ollaanko siihen kosketuksissa. groundLayer = lattiaksi asetettu gameObject, jolle on määritelty
        // Layeriksi groundLayer

        // tänne jotain toimintoa, kun painetaan Space. Kopioi valmis if tuolta alhaalta.

        return Physics2D.OverlapCircle(groundCheck.position, 4f, groundLayer);

    }
    bool IsOnGround()
    {
        // isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        return Physics2D.OverlapCapsule(groundCheck.position, new(1f, 2f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    bool IsOnJumpableObject()
    {
        // groundCheck.position antaa groundCheckin sijainnin. 4f on ympyrämitta, kuinka laajalta alueelta tarkistetaan
        // ground-alue ja ollaanko siihen kosketuksissa. groundLayer = lattiaksi asetettu gameObject, jolle on määritelty
        // Layeriksi groundLayer

        // t�nne jotain toimintoa, kun painetaan Space. Kopioi valmis if tuolta alhaalta.

        return Physics2D.OverlapCircle(groundCheck.position, 4f, jumpableObject);

    }

    // Tähän funktio, mikä liikuttaa pelaajaa. Aseta se Updateen.
    // Muista laittaa bracketit kiinni

    void Move()
    {
        if (rb != null)
        {
            // Flip the player's sprite based on direction of movement
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Face right
                direction = 1;

            }
            else if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Face left
                direction = -1;
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // move left:

            leftmove = -moveSpeed;
            rb.velocity = new(leftmove, rb.velocity.y);  // Huomaa, kuinka paljon smoothimpi liike RB:llä.
                                                         //  transform.Translate(leftmove * Time.deltaTime, 0, 0); // Tönkömpi liike


        }
        // move right:
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rightmove = moveSpeed;
            rb.velocity = new(rightmove, 0); // Smoothimpi liike
                                             //  transform.Translate(rightmove * Time.deltaTime, 0, 0); // Tönkömpi liike
        }
    }

    private void Jump()
    {
        //  if (rb != null && jump && IsOnGround() && jumpTime == 0)
        if (rb != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            //jumpTime += jumpTimeMultiplier * Time.deltaTime;
            //if (jumpTime == jumpMaxTime / 2 || IsOnGround())
            //{
            //    jumpTime = 0;
            //}
        }
    }
    void Update()
    {
        Move();
        Jump();
    }
}