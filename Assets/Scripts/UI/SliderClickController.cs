using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderClickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Slider slider;
    public bool clicking;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slider != null)
        {
            // Check if the pointer is over the slider
            if (RectTransformUtility.RectangleContainsScreenPoint(slider.fillRect, eventData.position))
            {
                clicking = true;
                Debug.Log("Clicked on the slider!");
                // Do whatever you want when the slider is clicked
            }
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (slider != null)
        {
            clicking = false;
        }
    }
}