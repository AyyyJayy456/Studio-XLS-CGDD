using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    [SerializeField] private Spells spellPrefab;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float rotateSpeed = 90f;
    private Vector3 axis = Vector3.up;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        sprite.sprite = spellPrefab.icon;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis * (rotateSpeed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        var hotbar = other.GetComponentInParent<Hotbar>();
        if (hotbar == null) return;

        if (hotbar.AddSpell(spellPrefab))
            Destroy(gameObject);
    }
}
