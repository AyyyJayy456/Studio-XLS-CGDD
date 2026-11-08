using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zap : Spells
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float expandTime = 0.3f;
    [SerializeField] private float stunDuration = 1f;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float shockwaveYOffset = -0.5f;
    [SerializeField] private GameObject shockwavePrefab;

    protected override void Use()
    {
        var hit = new HashSet<EnemyHealth>();

        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            var enemyShot = col.GetComponentInParent<EnemyProjectile>();
            if (enemyShot != null)
            {
                Destroy(enemyShot.gameObject);
                continue;
            }
            if (col.GetComponentInParent<Projectile>() != null) continue;
            var enemy = col.GetComponentInParent<EnemyHealth>();
            if (enemy == null || !hit.Add(enemy)) continue;

            enemy.TakeDamage(damage);
            if (enemy.TryGetComponent<StatusEffects>(out var fx))
            {
                fx.Stun(stunDuration);
            }
            if (enemy.TryGetComponent<Rigidbody>(out var rb) && !rb.isKinematic)
            {
                Vector3 dir = enemy.transform.position - transform.position;
                dir.y = 0f;
                rb.AddForce(dir.normalized * knockbackForce, ForceMode.VelocityChange);
            }

        }

        StartCoroutine(Shockwave());
    }

    private IEnumerator Shockwave()
    {
        var shockwave = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        var material = shockwave.GetComponentInChildren<Renderer>().material;
        Color baseColor = material.color;

        for (float t = 0; t < expandTime; t += Time.deltaTime)
        {
            float progress = t / expandTime;
            float currentRadius = Mathf.Lerp(0, radius, progress);
            shockwave.transform.localScale = new Vector3(currentRadius * 2, currentRadius * 2, currentRadius * 2);

            Color c = baseColor;
            c.a = baseColor.a * (1f - progress);
            material.color = c;

            yield return null;
        }
        Destroy(shockwave);
    }
}