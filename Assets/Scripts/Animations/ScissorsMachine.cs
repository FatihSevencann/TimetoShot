using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScissorsMachine : MonoBehaviour
{
    public float minAngle = -162f; // Minimum açı değeri
    public float maxAngle = 73f; // Maksimum açı değeri
    public float duration = 3f; // Dönüş süresi

    private void Start()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        transform.DORotate(new Vector3(0f, 0f, maxAngle), duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
               
                transform.DORotate(new Vector3(0f, 0f, minAngle), duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                       
                        RotateObject();
                    });
            });
    }
}
