using UnityEngine;

public class RavenBirds : MonoBehaviour
{
    public float flyingHeight = 0.15f;
    public Transform player;
    public Transform enemy;
    public float flySpeed = 3f;
    public float stopDistance = 0.5f;

    private bool shouldFollow = false;

    public void StartFollowing(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            shouldFollow = true;
        }
    }

    void Update()
    {
        if(enemy == null)
        {
            Destroy(gameObject);
            return;
        }
        if (!shouldFollow || player == null)
            return;

        Vector3 targetPosition = player.position;
        targetPosition.y = flyingHeight;

        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            direction.Normalize();

            Vector3 finalPosition =
                targetPosition - direction * stopDistance;

            transform.position = Vector3.MoveTowards(
                transform.position,
                finalPosition,
                flySpeed * Time.deltaTime
            );

            Quaternion targetRotation =
                Quaternion.LookRotation(direction) *
                Quaternion.Euler(0f, -90f, 0f);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                5f * Time.deltaTime
            );
        }
    }
}
