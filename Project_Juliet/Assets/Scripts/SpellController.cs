using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellController : MonoBehaviour
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float spellSpeed = 10f;
    [SerializeField] private float spellCooldown = 1f;
    [SerializeField] private float spellDamage = 10f;

    private float lastCastTime = -Mathf.Infinity;

    void Start()
    {

    }

    public void OnAttack(InputValue value)
    {
        if (Time.time >= lastCastTime + spellCooldown)
        {
            CastSpell();
            lastCastTime = Time.time;
        }
    }

    private void CastSpell()
    {
        GameObject spell = Instantiate(spellPrefab, firePoint.position, firePoint.rotation);

        if (spell.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.Initialize(spellDamage, spellSpeed, "Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
