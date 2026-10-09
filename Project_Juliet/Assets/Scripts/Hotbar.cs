using UnityEngine;
using UnityEngine.InputSystem;

public class Hotbar : MonoBehaviour
{

    [SerializeField] private Spells[] spells;
    [SerializeField] private GameObject[] selectionFrame;

    private int current;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Select(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Spells.inPauseMenu) return;
        var kb = Keyboard.current;

        for (int i = 0; i < spells.Length; i++)
        {
            if (kb.digit1Key.wasPressedThisFrame && i == 0) Select(i);
            if (kb.digit2Key.wasPressedThisFrame && i == 1) Select(i);
            if (kb.digit3Key.wasPressedThisFrame && i == 2) Select(i);
            if (kb.digit4Key.wasPressedThisFrame && i == 3) Select(i);
            if (kb.digit5Key.wasPressedThisFrame && i == 4) Select(i);
        }
    }

    public void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;
        if (current < spells.Length && spells[current] != null)
        {
            spells[current].CanCast();
        }
    }

    private void Select(int index)
    {
        if (index < 0 || index >= selectionFrame.Length) return;
        current = index;

        for (int i = 0; i < selectionFrame.Length; i++)
            if (selectionFrame[i] != null)
                selectionFrame[i].SetActive(i == current);
    }
}
