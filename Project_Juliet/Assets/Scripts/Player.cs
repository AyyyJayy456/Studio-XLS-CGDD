using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Camera playerCamera;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject winScreen;
    private float cameraAngle = 45f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 pointInput;
    private Vector3 lookTarget;
    private Quaternion cameraAng;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        cameraAng = Quaternion.Euler(0f, cameraAngle, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnPoint(InputValue value)
    {
        pointInput = value.Get<Vector2>();
    }


    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        Vector3 moveDirection = cameraAng * input;
        Vector3 moveVelocity = moveDirection * moveSpeed;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void RotatePlayer()
    {
        if (playerCamera == null) return;

        Ray ray = playerCamera.ScreenPointToRay(pointInput);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            lookTarget = hit.point;
        }
        else
        {
            Plane fallbackPlane = new Plane(Vector3.up, transform.position);
            if (fallbackPlane.Raycast(ray, out float distance))
                lookTarget = ray.GetPoint(distance);
        }

        Vector3 direction = lookTarget - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
