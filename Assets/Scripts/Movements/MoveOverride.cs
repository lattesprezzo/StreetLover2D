using UnityEngine;

public class MoveOverride : MonoBehaviour
{
    // Teleportaatioon tai erittäin tarkkaan sijoitteluun

    public float speed = 5f;

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        Vector3 newPos = transform.position + speed * 
            Time.deltaTime * new Vector3(inputX, 0, 0);
        transform.position = newPos;
    }
}