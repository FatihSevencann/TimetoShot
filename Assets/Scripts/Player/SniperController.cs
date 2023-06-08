using System;
using UnityEngine;

public class SniperController : Instancable<SniperController>
{
    public float rotationSpeed;
    

    public float rotationY = 0f; 
    private float rotationX = 0f; 
    

    

    [SerializeField] private Transform _crosshairTransfrom;

    private void Start()
    {
       
        rotationY = 10f;
    }
    
   

    private void Update()
    {
        
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
        
            rotationY += mouseX * rotationSpeed;
            rotationX += mouseY * rotationSpeed;
        
            rotationY = Mathf.Clamp(rotationY, LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].minRotationAngleY, LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].maxRotationAngleY);
            rotationX = Mathf.Clamp(rotationX, LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].minRotationAngleX, LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].maxRotationAngleX);

            if (Zoom.Instance.isShootin)
                transform.localRotation = Quaternion.Euler(15, rotationY, 1f);
            else
                transform.localRotation = _crosshairTransfrom.transform.localRotation;
        
            
        
          
      

       


    }
}
