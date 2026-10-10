using UnityEngine;

public class EntryTrigger : MonoBehaviour
{
    public RavenBirds[] birds;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (RavenBirds bird in birds)
            {
                if (bird != null)
                {
                    bird.StartFollowing(other);
                }
            }
        }
    }
}
