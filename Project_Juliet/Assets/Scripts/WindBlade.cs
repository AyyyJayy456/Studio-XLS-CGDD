using System.Collections.Generic;
using UnityEngine;

public class WindBlade : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    [SerializeField] private float hitRadius = 0.7f;
    [SerializeField] private float spinSpeed = 720f;

    private float damage, speed, range;
    private Vector3 startPos;
    private readonly HashSet<EnemyHealth> hit = new();

    public void Init(float damage, float speed, float range)
    {
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (sprite != null && spinSpeed != 0f)
            sprite.Rotate(0f, 0f, spinSpeed * Time.deltaTime, Space.Self);

        foreach (Collider col in Physics.OverlapSphere(transform.position, hitRadius))
        {
            if (col.isTrigger) continue;
            if (col.GetComponentInParent<PlayerHealth>() != null) continue;

            var enemy = col.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                if (hit.Add(enemy)) enemy.TakeDamage(damage);
                continue;
            }

            if (col.bounds.max.y < transform.position.y) continue; 
            Destroy(gameObject);
            return;
        }

        if (Vector3.Distance(startPos, transform.position) >= range)
            Destroy(gameObject);
    }
}
