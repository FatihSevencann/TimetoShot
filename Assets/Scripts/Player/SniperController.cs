using UnityEngine;

public class SniperController : MonoBehaviour
{
    public float rotationSpeed;
    public float minRotationAngle; 
    public float maxRotationAngle;

    private float rotationY = 0f; 

    [SerializeField] private Transform _crosshairTransfrom;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        
        rotationY += mouseX * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, minRotationAngle, maxRotationAngle);

        if (Zoom.Instance.isShootin)
            transform.localRotation = Quaternion.Euler(15f, rotationY, 1f);
        else
            transform.localRotation = _crosshairTransfrom.transform.localRotation;

    }
}
