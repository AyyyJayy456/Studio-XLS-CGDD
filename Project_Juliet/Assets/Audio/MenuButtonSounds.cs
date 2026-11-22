
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonSounds : MonoBehaviour,
    IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private MenuAudioManager audioManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioManager != null)
        {
            audioManager.PlayHoverSound();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioManager != null)
        {
            audioManager.PlayClickSound();
        }
    }
}
