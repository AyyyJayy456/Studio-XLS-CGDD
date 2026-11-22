using System.Collections;
using UnityEngine;

public class Rocky : MonoBehaviour
{
    public void Init(Vector3 show, float height, float rise, float duration, float leave)
    {
        StartCoroutine(Lifetime(show, height, rise, duration, leave));
    }

    private IEnumerator Lifetime(Vector3 show, float height, float rise, float duration, float leave)
    {
        Vector3 hide = show + Vector3.down * height;

        for (float t = 0f; t < rise; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(hide, show, t / rise);
            yield return null;
        }
        transform.position = show;

        yield return new WaitForSeconds(duration);

        for (float t = 0f; t < leave; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(show, hide, t / leave);
            yield return null;
        }
        Destroy(gameObject);
    
    }
}
