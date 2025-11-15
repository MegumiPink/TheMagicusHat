using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Ataque")]
    public Transform player;              
    public GameObject projectilePrefab;   
    public float detectionRange = 8f;     
    public float shootInterval = 1.2f;    
    public float projectileSpeed = 12f;   
    private float _nextShootTime;

    void Start()
    {
        if (currentHealth <= 0) currentHealth = maxHealth;

        if (player == null)
        {
            var tagged = GameObject.FindGameObjectWithTag("Player");
            if (tagged != null) player = tagged.transform;
        }
    }

    void Update()
    {
        shootslimeball();
    }

    public void shootslimeball()
        {
        if (player == null || projectilePrefab == null) return;

        Vector2 origin = transform.position;
        Vector2 toTarget = (Vector2)player.position - origin;
        float distance = toTarget.magnitude;

        if (distance > detectionRange) return;
        if (Time.time < _nextShootTime) return;

        Vector2 dir = toTarget.normalized;

        GameObject proj = Instantiate(projectilePrefab, origin, Quaternion.identity);

        if (proj.tag != "EnemyProjectile")
            proj.tag = "EnemyProjectile";

        var rb2d = proj.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.linearVelocity = dir * projectileSpeed;
            rb2d.gravityScale = 0f;
        }
        else
        {
            var rb = proj.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = new Vector3(dir.x, dir.y, 0f) * projectileSpeed;
        }

        Destroy(proj, 5f);
        _nextShootTime = Time.time + shootInterval;

    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

 
    void OnDrawGizmosSelected() // see range in editor
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
