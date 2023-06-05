using UnityEngine;
using DG.Tweening;
using Helpers;

public class CameraSwitchAnimator : Instancable<CameraSwitchAnimator>
{
    [SerializeField] private Transform arrowPosTransform;
    [SerializeField] public Transform thirdPersonPosTransform;

    public bool IsCameraMoving;
    private void Start()
    {
        SwitchToThirdPersonPos();
    }

    private void Update()
    {
        if (Zoom.Instance.zoomSlider.value<60)
        {
            Zoom.Instance.isShootin = false;
            SwitchToArrowPos();
        }
        if (Zoom.Instance.zoomSlider.value>=60 && !FindObjectOfType<Bullet>())
        {
            Zoom.Instance.isShootin = true;
            SwitchToThirdPersonPos();
        }
    }
    public void SwitchToArrowPos()
    {
        IsCameraMoving = true;
        StaticObjects.MainCamera.transform.DOMove(arrowPosTransform.position, 1).OnComplete((() =>
        {
            IsCameraMoving = false;
        }));
        StaticObjects.MainCamera.transform.parent = null;
        AimController.Instance.IsAiming = true;
    }
    public void SwitchToThirdPersonPos()
    {
        IsCameraMoving = true;
        Transform transform1;
        
        (transform1 = StaticObjects.MainCamera.transform).DOMove(thirdPersonPosTransform.position, 1).OnComplete(() =>
        {
            IsCameraMoving = false;
        });
        
        transform1.parent = thirdPersonPosTransform;
        transform1.localPosition = Vector3.zero;
        transform1.localRotation =Quaternion.identity;
        
        AimController.Instance.IsAiming = false;
    }
}
