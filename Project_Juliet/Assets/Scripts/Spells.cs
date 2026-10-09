using UnityEngine;

public abstract class Spells : MonoBehaviour
{
   public string spellName;
    public Sprite icon;
    [SerializeField] protected float cooldown = 1f;

    private float lastCastTime = -Mathf.Infinity;

    public bool CanCast()
    {
        if (Time.time < lastCastTime + cooldown)
        {
            return false;
        }
        lastCastTime = Time.time;
        Use();
        return true;
    }

    protected abstract void Use();
}
