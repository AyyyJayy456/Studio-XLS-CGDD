using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class StatusEffects : MonoBehaviour
{
    private EnemyHealth health;
    private float slowDur, stunDur, slowFactor = 1f;
    private Coroutine burn;

    public bool IsStunned => Time.time < stunDur;
    public float SpeedMulti => Time.time < slowDur ? slowFactor : 1f;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }

    public void Slow(float factor, float duration)
    {
        slowFactor = factor;
        slowDur = Time.time + duration;
    }
    
    public void Stun(float duration)
    {
        stunDur = Time.time + duration;
    }

    public void Burn(float damage, float duration)
    {
        if (burn != null)
            StopCoroutine(burn);
        burn = StartCoroutine(BurnCoroutine(damage, duration));
    }

    private IEnumerator BurnCoroutine(float damage, float duration)
    {
        for (float t = 0; t < duration; t += 0.5f)
        {
            yield return new WaitForSeconds(0.5f);
            if (health != null) health.TakeDamage(damage * 0.5f);
        }
        burn = null;
    }
}
