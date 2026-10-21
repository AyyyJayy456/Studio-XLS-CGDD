using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    [SerializeField] private Spells spellPrefab;
    
    private
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var hotbar = other.GetComponentInParent<Hotbar>();
        if (hotbar == null) return;

        if (hotbar.AddSpell(spellPrefab))
            Destroy(gameObject);
    }
}
