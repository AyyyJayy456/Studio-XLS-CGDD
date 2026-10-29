using UnityEngine;
using UnityEngine.EventSystems;

public class HintHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hint;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovered");
        hint.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hint.SetActive(false);
    }
}
