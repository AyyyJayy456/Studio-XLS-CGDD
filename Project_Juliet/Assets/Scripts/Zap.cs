using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zap : Spells
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float damage = 15f;

    protected override void Use()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            var enemy = col.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Flash();
    }

    private void Flash()
    {
        var flash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(flash.GetComponent<Collider>());
        flash.transform.position = transform.position;
        flash.transform.localScale = Vector3.one * radius * 2f;
        flash.GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f, 0.4f);
        Destroy(flash, 0.2f);
    }
}