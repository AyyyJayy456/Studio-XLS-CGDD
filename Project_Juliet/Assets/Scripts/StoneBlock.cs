using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class StoneBlock : Spells
{

    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private Vector3 size = new(3f, 2f, 0.8f);
    [SerializeField] private float distance = 2.5f;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float castTime = 0.2f;
    [SerializeField] private float leaveTime = 0.4f;

    protected override void Use()
    {
        var player = GetComponentInParent<Player>();

        Transform p = player != null ? player.transform : transform;

        Vector3 forward = p.forward;
        forward.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(forward);
        Vector3 show = p.position + forward * distance;
        GameObject rock = Instantiate(rockPrefab, show, rotation);
        rock.transform.localScale = size;

        rock.name = "StoneBlock";
        if (!rock.TryGetComponent<Rigidbody>(out var rb)) rb = rock.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        rock.AddComponent<Rocky>().Init(show, size.y, castTime, duration, leaveTime);
    }
}
