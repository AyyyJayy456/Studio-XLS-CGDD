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

    private int shots;
    private float regenTimer;


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
        Debug.Log("shots fired: " + shots);
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
                projectile.Initialize(spellDamage, spellSpeed, "Enemy");
        }
    }
}
