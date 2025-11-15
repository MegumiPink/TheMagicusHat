using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Aurora Speed")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    Vector2 movement;

    [Header("Aurora Life")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Respawn")]
    [SerializeField] private bool enableRespawnCheat = true;
    private Vector2 startPosition;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        currentHealth = maxHealth;

        // star position in the world(spawn)
        startPosition = rb.position;
    }

    void Update()
    {
        // Cheat to enable respawn inicial position in the map
        if (enableRespawnCheat && Input.GetKeyDown(KeyCode.R))
        {
            RespawnAtStart();
        }

        if (PauseController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero; //Stop Movement
            //animator.SetBool("isWalking", false);
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int amount)
    {
        
        if (currentHealth <= 0 ) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        RespawnAtStart();
    }

    private void RespawnAtStart()
    {
        
        currentHealth = maxHealth;
        movement = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.position = startPosition;          
        transform.position = startPosition;   

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = collision.collider;

        if (col.CompareTag("EnemyProjectile"))
        {
            TakeDamage(1);
            Destroy(col.gameObject);
            return;
        }

        if (col.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
}