using UnityEngine;

public abstract class Spells : MonoBehaviour
{
    public string spellName;
    public Sprite icon;
    public static bool inPauseMenu;
    [SerializeField] protected float cooldown = 1f;

    private float lastCastTime = -Mathf.Infinity;

    public bool CanCast()
    {
        if (inPauseMenu)
        {
            return false;
        }
        if (Time.time < lastCastTime + cooldown)
        {
            return false;
        }
        lastCastTime = Time.time;
        Use();
        return true;
    }

    public virtual float CooldownFill
    {
        get 
        { 
            if (cooldown <= 0f)
            {
                return 0f;
            }
            return Mathf.Clamp01(1f - (Time.time - lastCastTime) / cooldown);
        }
    }
    protected abstract void Use();
}
