using UnityEngine;

public class BarHits : MonoBehaviour
{
    private int hitCount = 0;
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;
    public Animator anim5;
    public Collider barCollider;
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
        if (other.gameObject.CompareTag("Projectile"))
        {
            hitCount++;
            Debug.Log("Hit count: " + hitCount);
            if (hitCount == 1)
            {
                anim1.SetTrigger("1stHit");
            }
            else if (hitCount == 2)
            {
                anim2.SetTrigger("2ndHit");
            }
            else if (hitCount == 3)
            {
                anim3.SetTrigger("3rdHit");
            }
            else if (hitCount == 4)
            {
                anim4.SetTrigger("4thHit");
            }
            else if (hitCount == 5)
            {
                anim5.SetTrigger("5thHit");
                barCollider.enabled = false;
            }
        }
    }

}

