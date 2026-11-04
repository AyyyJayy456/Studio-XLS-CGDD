using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfRoom : MonoBehaviour
{
    [SerializeField] private GameObject fogCover;
    [SerializeField] private float fadeTime = 1f;

    private Canvas[] hiddenHealth;

    private bool revealed;

    void Start()   
    {
        var area = GetComponent<Collider>();
        var list = new List<Canvas>();

        foreach (var e in FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None))
        {
            Vector3 p = e.transform.position;
            p.y = area.bounds.center.y;
            if (!area.bounds.Contains(p)) continue;

            var bar = e.GetComponentInChildren<Canvas>(true);
            if (bar != null)
            {
                bar.enabled = false;
                list.Add(bar);
            }
        }

        hiddenHealth = list.ToArray();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (revealed) return;
        if (other.GetComponentInParent<Player>() == null) return;
        if (DeleteProjectile(other)) return;

        revealed = true;
        StartCoroutine(Reveal());

    }

    private void OnTriggerStay(Collider other)
    {
        if (revealed) return;
        DeleteProjectile(other);

    }
    private IEnumerator Reveal()
    {
        var renderer = fogCover.GetComponentsInChildren<Renderer>();
        var mats = new Material[renderer.Length];
        var baseCol = new Color[renderer.Length];

        foreach (var bar in hiddenHealth)
            if (bar != null) bar.enabled = true;

        for (int i = 0; i < renderer.Length; i++)
        {
            mats[i] = renderer[i].material;
            baseCol[i] = mats[i].color;
        }

        for (float t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            float k = t / fadeTime;
            for (int i = 0; i < mats.Length; i++)
            {
                Color c = baseCol[i];
                c.a = baseCol[i].a * (1f - k);
                mats[i].color = c;

            }

            yield return null;
        }

        Destroy(fogCover);
    }

    private bool DeleteProjectile(Collider other)
    {
        var proj = other.GetComponentInParent<Projectile>();
        if (proj != null)
        {
            Destroy(proj.gameObject);
            return true;
        }
        var enemyProj = other.GetComponentInParent<EnemyProjectile>();
        if (enemyProj != null)
        {
            Destroy(enemyProj.gameObject);
            return true;
        }
        return false;
    }
}
