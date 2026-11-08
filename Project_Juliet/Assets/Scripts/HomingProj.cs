using UnityEngine;

public class HomingProj : MonoBehaviour
{
    [SerializeField] private float range = 8f;
    [SerializeField] private float maxAngle = 50f;
    [SerializeField] private float turnSpeed = 90f;

    private Transform target;
    private float nextSearch;

    private void FixedUpdate()
    {
        if (target == null)
        {
            if (Time.time < nextSearch) return;
            target = FindTarget();
            nextSearch = Time.time + 0.5f;
            if (target == null) return;
        }

        Vector3 dir = (target.position - transform.position);
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.01f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, turnSpeed * Time.fixedDeltaTime);
    }

    private Transform FindTarget()
    {
        EnemyHealth best = null;
        float bestAngle = maxAngle;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        foreach (var e in FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None))
        {
            Vector3 to = e.transform.position - transform.position;
            to.y = 0f;
            if (to.magnitude > range) continue;

            float angle = Vector3.Angle(transform.forward, to);
            if (angle > bestAngle) continue;


            if (Physics.Linecast(origin, e.transform.position + Vector3.up * 0.5f, out RaycastHit hit,
                        ~0, QueryTriggerInteraction.Ignore)
                    && hit.transform.GetComponentInParent<EnemyHealth>() != e)
                continue;

            bestAngle = angle;
            best = e;
        }
        return best != null ? best.transform : null;
    }
}

