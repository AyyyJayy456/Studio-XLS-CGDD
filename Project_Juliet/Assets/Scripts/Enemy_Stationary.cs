using UnityEngine;

public class Enemy_Stationary : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float projectileDamage = 10f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private Rigidbody rb;
    private Transform playerTarget;
    private float nextFireTime;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.isKinematic = true; 
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTarget = playerObj.transform;
        }
    }

    void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (playerTarget == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerTarget = playerObj.transform;
            return;
        }

        float distance = Vector3.Distance(transform.position, playerTarget.position);
        if (distance > detectionRadius) return;

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || playerTarget == null) return;

        Vector3 origin = (firePoint != null) ? firePoint.position : transform.position;

        // Calculate direction toward player on the horizontal plane
        Vector3 aimDirection = (playerTarget.position - origin);
        aimDirection.y = 0f;

        if (aimDirection.sqrMagnitude < 0.001f) return;

        aimDirection.Normalize();

        Vector3 spawnPos = origin + (aimDirection * 0.8f);
        spawnPos.y = origin.y;

        Quaternion bulletRotation = Quaternion.LookRotation(aimDirection);
        GameObject bullet = Instantiate(projectilePrefab, spawnPos, bulletRotation);

        if (bullet.TryGetComponent<Projectile>(out var projectile))
        {
            projectile.Initialize(projectileDamage, projectileSpeed, "Player");
        }
    }
}