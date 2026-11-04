using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyProjectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    public void Initialize(float damageAmount, float moveSpeed, float lifetime = 5f)
    {
        damage = damageAmount;
        speed = moveSpeed;
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        if (rb != null && !rb.isKinematic)
        {
            rb.linearVelocity = transform.forward * speed;
        }
        else
        {
            transform.position += speed * Time.fixedDeltaTime * transform.forward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        if (other.GetComponentInParent<EnemyProjectile>() != null) return;
        if (other.GetComponentInParent<Projectile>() != null) return;
        if (other.GetComponentInParent<EnemyHealth>() != null) return;

        var player = other.GetComponentInParent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
