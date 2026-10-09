using System.Collections;
using UnityEngine;

public class Zap : Spells
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float expandTime = 0.3f;
    [SerializeField] private GameObject shockwavePrefab;

    protected override void Use()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            var projectile = col.GetComponentInParent<Projectile>();
            if (projectile != null)
            {
                if (projectile.TargetTag == "Player")
                {
                    Destroy(projectile.gameObject);
                    continue;
                }
            }
            var enemy = col.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        StartCoroutine(Shockwave());
    }

    private IEnumerator Shockwave()
    {
        var shockwave = Instantiate(shockwavePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
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