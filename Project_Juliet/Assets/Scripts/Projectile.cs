using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    public float burnDps, burnDuration, slowFactor = 1f, slowDuration, stunDuration;
    public float fallOffDistance;
    public float minFalloff = 0.3f;
    public float blastRadius, blastFallOff = 0.5f;
    private Rigidbody rb;
    private Vector3 startPos;

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
        startPos = transform.position;
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
            transform.position += speed * Time.fixedDeltaTime * transform.forward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        if (other.GetComponentInParent<Projectile>() != null) return;
        if (other.GetComponentInParent<EnemyProjectile>() != null) return;
        if (other.GetComponentInParent<PlayerHealth>() != null) return;

        var enemy = other.GetComponentInParent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(CalcDamage());
            StatusProc(enemy);
            Explode(enemy);
        }
        else
        {
            Explode(null);
        }
        Destroy(gameObject);
    }

    private float CalcDamage()
    {
        if (fallOffDistance <= 0f) return damage;
        float t = Mathf.Clamp01(Vector3.Distance(startPos, transform.position) / fallOffDistance);
        return damage * Mathf.Lerp(1f, minFalloff, t);
    }

    private void StatusProc(EnemyHealth enemy)
    {
        if (!enemy.TryGetComponent<StatusEffects>(out var fx)) return;
        if (burnDuration > 0f) fx.Burn(burnDps, burnDuration);
        if (slowDuration > 0f) fx.Slow(slowFactor, slowDuration);
        if (stunDuration > 0f) fx.Stun(stunDuration);
    }

    private void Explode(EnemyHealth enemy)
    {
        if (blastRadius <= 0f) return;
        var hit = new HashSet<EnemyHealth>();
        if (enemy != null) hit.Add(enemy);

        foreach (Collider col in Physics.OverlapSphere(transform.position, blastRadius))
        {
            var e = col.GetComponentInParent<EnemyHealth>();
            if (e == null || !hit.Add(e)) continue;

            e.TakeDamage(damage * blastFallOff);
            StatusProc(e);
        }

        var flash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(flash.GetComponent<Collider>());
        flash.transform.position = transform.position;
        flash.transform.localScale = 2f * blastRadius * Vector3.one;
        var mat = new Material(Shader.Find("Sprites/Default"))
        {
            color = new Color(1f, 0.5f, 0.1f, 0.5f)
        };
        flash.GetComponent<Renderer>().material = mat;
        Destroy(flash, 0.15f);
    }
}