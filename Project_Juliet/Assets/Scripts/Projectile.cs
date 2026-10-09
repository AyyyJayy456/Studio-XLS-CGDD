using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private string targetTag = "Enemy";
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }
    public void Initialize(float damageAmount, float moveSpeed, string enemyTag = "Enemy", float lifetime = 5f)
    {
        damage = damageAmount;
        speed = moveSpeed;
        targetTag = enemyTag;
        Destroy(gameObject, lifetime);
    }
    void FixedUpdate()
    {
        if (rb != null && !rb.isKinematic)
        {
            rb.linearVelocity = transform.forward * speed;
        }
        else
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            if (other.gameObject.TryGetComponent<PlayerHealth>(out var player))
            {
                player.TakeDamage(damage);
            }
            else if (other.gameObject.TryGetComponent<EnemyHealth>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}