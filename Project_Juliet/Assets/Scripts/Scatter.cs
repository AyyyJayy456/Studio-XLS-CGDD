using System;
using UnityEngine;

public class Scatter : Spells
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float spellSpeed = 10f;
    [SerializeField] private float spellDamage = 5f;


    [SerializeField] private int maxProjectiles = 5;
    [SerializeField] private float regenTime = 1f;
    [SerializeField] private float anglePer = 10f;

    [SerializeField] private float range = 10f;
    [SerializeField] private float minFalloff = 0.3f;
    [SerializeField] private float slowFactor = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    private int shots;
    private float regenTimer;

    private void Awake()
    {
        if (firePoint == null) firePoint = transform.parent;
    }
    

    private void Start()
    {
        shots = maxProjectiles;
    }

    private void Update()
    {
        if (shots >= maxProjectiles)
        { 
            regenTimer = 0f;
            return;
        }

        regenTimer += Time.deltaTime;
        if (regenTimer >= regenTime)
        {
            regenTimer -= regenTime;
            shots++;
        }
    }

    protected override void Use()
    {
        if (shots <= 0) return;

        int count = shots;
        shots = 0;
        regenTimer = 0f;
        
        float start = -anglePer * (count - 1) / 2f;

        for (int i = 0; i < count; i++)
        {
            Quaternion rot = firePoint.rotation * Quaternion.Euler(0f, start + anglePer * i, 0f);
            GameObject spell = Instantiate(spellPrefab, firePoint.position, rot);

            if (spell.TryGetComponent<Projectile>(out var projectile))
            {
                projectile.Initialize(spellDamage, spellSpeed);
                projectile.fallOffDistance = range;
                projectile.minFalloff = minFalloff;
                projectile.slowFactor = slowFactor;
                projectile.slowDuration = slowDuration;
            }
        }
    }
}
