using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fireball : Spells
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float spellSpeed = 10f;
    [SerializeField] private float spellDamage = 10f;

    protected override void Use()
    {
        GameObject spell = Instantiate(spellPrefab, firePoint.position, firePoint.rotation);
        if (spell.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.Initialize(spellDamage, spellSpeed, "Enemy");
        }
    }
}
