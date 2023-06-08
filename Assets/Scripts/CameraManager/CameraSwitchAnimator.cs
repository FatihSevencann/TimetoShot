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
        SwitchToThirdPersonPos(0);
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

    public void Recoil()
    {
        StaticObjects.MainCamera.transform.DOShakePosition(0.2f, new Vector3(0.1f, 0.1f, 0f));
    }
    public void SwitchToThirdPersonPos(float time = 1)
    {
        IsCameraMoving = true;
        Transform transform1;

        StaticObjects.MainCamera.transform.DOKill();
        
        (transform1 = StaticObjects.MainCamera.transform).DOMove(thirdPersonPosTransform.position, time).OnComplete(() =>
        {
            IsCameraMoving = false;
        });
        
        transform1.parent = thirdPersonPosTransform;
        transform1.localPosition = Vector3.zero;
        transform1.localRotation =Quaternion.identity;
        
        AimController.Instance.IsAiming = false;
    }
}
