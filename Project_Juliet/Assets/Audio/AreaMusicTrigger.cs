
using UnityEngine;

public class AreaMusicTrigger : MonoBehaviour
{
    public enum AreaType
    {
        Garden,
        Cave
    }

    [Header("Area Settings")]
    [SerializeField] private AreaType areaType;

    [SerializeField] private AreaAudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (audioManager == null)
        {
            Debug.LogWarning("Area Audio Manager is missing!");
            return;
        }

        switch (areaType)
        {
            case AreaType.Garden:
                audioManager.PlayGardenMusic();
                break;

            case AreaType.Cave:
                audioManager.PlayCaveMusic();
                break;
        }
    }
}
