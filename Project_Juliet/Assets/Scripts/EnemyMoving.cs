using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMoving : MonoBehaviour
{
    [Header("Orbit Movement")]     
    [SerializeField] private float orbitRadius = 5f;      
    [SerializeField] private float orbitSpeed = 40f;    
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float rotationSpeed = 15f;   

    [Header("Combat & Detection")]
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float projectileDamage = 10f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private StatusEffects status;
    private Rigidbody rb;
    private Transform playerTarget;
    private float nextFireTime;
    private Vector3 centerPoint;
    private float currentAngle;

    private bool Stunned => status != null && status.IsStunned;
    private float SpeedMulti => status != null ? status.SpeedMulti : 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.isKinematic = true;
        }
        status = GetComponent<StatusEffects>();

        FindPlayer();
        centerPoint = transform.position;
    }

    void Update()
    {
        HandleMovement();
        HandleFacingPlayer();
        HandleShooting();
    }
    private void HandleMovement()
    {
        if (Stunned) return;

        float dir = clockwise ? -1f : 1f;
        currentAngle += dir * orbitSpeed * SpeedMulti * Time.deltaTime;
        currentAngle %= 360f;
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)) * orbitRadius;

        transform.position = centerPoint + offset;
    }
    private void HandleFacingPlayer()
    {
        if (playerTarget == null) return;

        Vector3 lookDir = playerTarget.position - transform.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleShooting()
    {
        if (playerTarget == null)
        {
            FindPlayer();
            return;
        }

        float distance = Vector3.Distance(transform.position, playerTarget.position);
        if (distance > detectionRadius) return;

        if (Stunned) return;

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (1f / (fireRate * SpeedMulti));
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || playerTarget == null) return;

        Vector3 origin = (firePoint != null) ? firePoint.position : transform.position;

        Vector3 aimDirection = (playerTarget.position - origin);
        aimDirection.y = 0f;

        if (aimDirection.sqrMagnitude < 0.001f) return;

        aimDirection.Normalize();

        Vector3 spawnPos = origin + (aimDirection * 0.8f);
        spawnPos.y = origin.y;

        Quaternion bulletRotation = Quaternion.LookRotation(aimDirection);
        GameObject bullet = Instantiate(projectilePrefab, spawnPos, bulletRotation);

        if (bullet.TryGetComponent<EnemyProjectile>(out var projectile))
        {
            projectile.Initialize(projectileDamage, projectileSpeed);
        }
    }
    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTarget = playerObj.transform;
        }
    }
}