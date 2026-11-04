using UnityEngine;

public class Fireball : Spells
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float spellSpeed = 6f;
    [SerializeField] private float spellDamage = 20f;

    [SerializeField] private float burnDps = 4f;
    [SerializeField] private float burnDuration = 3f;

    [SerializeField] private float blastRadius = 3f;
    [SerializeField] private float lifetime = 3f;

    private void Awake()
    {
        if (firePoint == null) firePoint = transform.parent;  
    }
    protected override void Use()
    {
        GameObject spell = Instantiate(spellPrefab, firePoint.position, firePoint.rotation);
        if (spell.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.Initialize(spellDamage, spellSpeed, lifetime);
            projectile.burnDps = burnDps;
            projectile.burnDuration = burnDuration;
            projectile.blastRadius = blastRadius;
            projectile.blastFallOff = 0.5f;
        }
    }
}
