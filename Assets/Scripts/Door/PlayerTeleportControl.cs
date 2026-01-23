using UnityEngine;

public class PlayerTeleportControl : MonoBehaviour
{
    public Transform startSpawnLocation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransportPlayer(other);

        }
    }

    void TransportPlayer(Collider2D other)
    {
           // Debug.Log(other.gameObject.name + " should disappear!");
           // other.gameObject.SetActive(false);

            other.gameObject.transform.position = startSpawnLocation.position;

            //2 keinoa pys‰ytt‰‰ pelaajan liike:
            // - Suoraan referointi Rigidbody2D:n kautta
            //other.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            // - Collider2D:n kautta. Luodaan v‰liaikainen Rigidbody2D-muuttuja
            Rigidbody2D rb = other.attachedRigidbody;
            rb.linearVelocity = Vector2.zero;
    }


}
