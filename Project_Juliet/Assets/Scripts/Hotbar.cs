using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{

    [SerializeField] private Spells[] spells;
    [SerializeField] private Image[] slotIcons;
    [SerializeField] private Image[] slotBackgrounds;
    [SerializeField] private Color emptySlotColor = new(0.2f, 0.2f, 0.2f, 1f);
    [SerializeField] private Color filledSlotColor = Color.white;
    [SerializeField] private GameObject[] selectionFrame;
    [SerializeField] private Transform spellParent;

    private int current;

    void Awake()
    {
        if (spells == null) spells = new Spells[selectionFrame.Length];
        else if (spells.Length != selectionFrame.Length) Array.Resize(ref spells, selectionFrame.Length);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshIcons();
        Select(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Spells.inPauseMenu) return;
        
        var kb = Keyboard.current;

        if (kb.digit1Key.wasPressedThisFrame) Select(0);
        if (kb.digit2Key.wasPressedThisFrame) Select(1);
        if (kb.digit3Key.wasPressedThisFrame) Select(2);
        if (kb.digit4Key.wasPressedThisFrame) Select(3);
        if (kb.digit5Key.wasPressedThisFrame) Select(4);
    }

    public void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;
        if (current < spells.Length && spells[current] != null)
        {
            spells[current].CanCast();
        }
    }

    public bool AddSpell(Spells spellPrefab, bool equip = true)
    {
        if (spellPrefab == null) return false;

        for (int i = 0; i < spells.Length; i++)
            if (spells[i] != null && spells[i].GetType() == spellPrefab.GetType())
                return false;

        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i] != null) continue;

            spells[i] = Instantiate(spellPrefab, spellParent != null ? spellParent : transform);
            spells[i].transform.localPosition = Vector3.zero;
            spells[i].transform.localRotation = Quaternion.identity;
            RefreshIcons();

            if (equip) Select(i);
            return true;
        }

        return false;
    }
    private void RefreshIcons()
    {
        for (int i = 0; i < spells.Length; i++)
        {
            bool hasSpell = spells[i] != null;

            if (i < slotIcons.Length && slotIcons[i] != null)
            {
                bool hasIcon = hasSpell && spells[i].icon != null;
                slotIcons[i].gameObject.SetActive(hasIcon);
                slotIcons[i].sprite = hasIcon ? spells[i].icon : null;
            }

            if (slotBackgrounds != null && i < slotBackgrounds.Length && slotBackgrounds[i] != null)
            {
                slotBackgrounds[i].color = hasSpell ? filledSlotColor : emptySlotColor;
            }
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
