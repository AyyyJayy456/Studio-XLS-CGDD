using System.Collections;
using UnityEngine;

public class FogOfRoom : MonoBehaviour
{
    [SerializeField] private GameObject fogCover;
    [SerializeField] private float fadeTime = 1f;

    private bool revealed;

    private void OnTriggerEnter(Collider other)
    {
        if (revealed) return;
        if (other.GetComponentInParent<Player>() == null) return;

        revealed = true;
        StartCoroutine(Reveal());

    }
    private IEnumerator Reveal()
    {
        var renderer = fogCover.GetComponentsInChildren<Renderer>();
        var mats = new Material[renderer.Length];
        var baseCol = new Color[renderer.Length];

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
}
