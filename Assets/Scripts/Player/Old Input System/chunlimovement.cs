using UnityEngine;

public class Chunlimovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    bool hideKeyPressed = false;
    SpriteRenderer sr;
    Animator animator;

    [SerializeField]    
    private Vector2 movement;

    void HideChunLi() {
        hideKeyPressed = Input.GetKeyDown(KeyCode.X);

        if (hideKeyPressed) {
            // Piilota objekti:
            //gameObject.SetActive(false);

            // Tuhoa objekti kokonaan:
            // Destroy(gameObject);

            // Poista vain näkyvyys:
            sr = GetComponent<SpriteRenderer>(); // Haetaan komponentti
            sr.enabled = !sr.enabled; // Piilotetaan/näytetään sprite
            // Checkataan, onko animator-komponentti olemassa:
            //animator = GetComponent<Animator>();
            //--------------------
            //if(animator == null)
            //{
            //    Debug.Log("Animator component not found!" );    
            //}
            // --------------------
            if(TryGetComponent<Animator>(out animator))
            {
                Debug.Log("Animator component found!" );    
            }
            else
            {
                Debug.Log("Animator component not found!" );
                animator = gameObject.AddComponent<Animator>(); // Luodaan uusi Animator-komponentti
                Debug.Log("Animator component added succesfully!" );
                animator.enabled = false; // Poistetaan käytöstä    
            }


        
        }

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        rb.MovePosition(rb.position + movement.normalized *
            moveSpeed * Time.deltaTime);

        HideChunLi();
    }
}
