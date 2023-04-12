using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueWindow : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
