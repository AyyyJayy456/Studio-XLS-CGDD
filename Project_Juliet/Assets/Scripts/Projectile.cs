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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged") && other.gameObject.layer != LayerMask.NameToLayer("Default"))
        {
            return;
        }

        // Hit the designated target
        if (other.CompareTag(targetTag))
        {
            // Futue HEALTH : other.GetComponent<Health>()?.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Hit level geometry or walls
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Destroy(gameObject);
        }
    }
}