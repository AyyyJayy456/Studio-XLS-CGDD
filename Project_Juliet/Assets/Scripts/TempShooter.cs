using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempShooter : MonoBehaviour
{
    [SerializeField] private Spells[] spells;
    private int current;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var kb = Keyboard.current;

        if (kb.digit1Key.wasPressedThisFrame && spells.Length > 0)
        {
            current = 0;
        }
        else if (kb.digit2Key.wasPressedThisFrame && spells.Length > 1)
        {
            current = 1;
        }
        else if (kb.digit3Key.wasPressedThisFrame && spells.Length > 2)
        {
            current = 2;
        }
    }

    public void OnAttack(InputValue value)
    {
        if (spells.Length > 0 && spells[current] != null)
        {
            if (spells[current].CanCast())
            {
                Debug.Log($"Casting spell: {spells[current].spellName}");
            }
            else
            {
                Debug.Log($"Spell {spells[current].spellName} is on cooldown.");
            }
        }
    }
}
