using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveVelocity : MonoBehaviour
{
    [Space(10)]
    [Header("Script Usage")]
    [TextArea(3, 10)]
    public string info;
    // Top-down movements or zero-gravity environments

    public float speed = 10f;
    private Rigidbody2D rb;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);
    }
}