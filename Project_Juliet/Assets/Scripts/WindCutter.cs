using System.Collections;
using UnityEngine;

public class WindCutter : Spells
{

    [SerializeField] private Transform firePoint;
    [SerializeField] private WindBlade bladePrefab;
    [SerializeField] private int blades = 3;
    [SerializeField] private float delay = 0.12f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 22f;
    [SerializeField] private float range = 15f;

    private void Awake()
    {
        if (firePoint == null) firePoint = transform.parent;
    }

    protected override void Use()
    {
        StartCoroutine(Volley());
    }
    private IEnumerator Volley()
    {
        for (int i = 0; i < blades; i++)
        {
            WindBlade blade = Instantiate(bladePrefab, firePoint.position, firePoint.rotation);
            blade.Init(damage, speed, range);

            if (i < blades - 1) yield return new WaitForSeconds(delay);
        }
    }
}
