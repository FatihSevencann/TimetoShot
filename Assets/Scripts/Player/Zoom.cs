using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Zoom : Instancable<Zoom>
{
    private UnityEngine.CharacterController _controller;

    [SerializeField] private Transform _sniperTransfrom;
    [SerializeField] float minFOV ;
    [SerializeField] float maxFOV;

    public Camera mainCamera;
    public Slider zoomSlider;
    public bool isShootin = false;
    
    
    private void Awake()
    {
        zoomSlider.minValue = minFOV;
        zoomSlider.maxValue = maxFOV;
        zoomSlider.value = maxFOV;
        _controller = GetComponent<UnityEngine.CharacterController>();
    }

    public void OnZoomSliderValueChanged()
    {
        if (!isShootin )
        {
            float fov = zoomSlider.value;
            mainCamera.fieldOfView = fov;
        }
    }
    private void Update()
    {

        if (CameraSwitchAnimator.Instance.IsCameraMoving)
        {
            return;
        }
    }
  
}