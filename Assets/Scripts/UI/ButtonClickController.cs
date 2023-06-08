using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    
    private Button fireButton;
    public bool clicking;

    private RectTransform _rect;
    
    private void Awake()
    {
        fireButton = GetComponent<Button>();
        _rect = fireButton.GetComponent<RectTransform>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (fireButton != null)
        {
          
            if (RectTransformUtility.RectangleContainsScreenPoint(_rect, eventData.position))
            {
                clicking = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (fireButton != null)
        {
            clicking = false;
        }
    }
}
