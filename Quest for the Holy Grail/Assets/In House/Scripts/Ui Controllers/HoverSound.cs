using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSound : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponentInParent<CanvasGroup>().alpha == 1)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
