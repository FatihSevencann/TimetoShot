using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FerrisWhellAnimation : MonoBehaviour
{
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private float rotateSpeed;

    private void Start()
    {
        transform.DOLookAt(rotationPoint.position, rotateSpeed, AxisConstraint.None);

        transform.DORotate(new Vector3(0f, 0f, 360f), rotateSpeed, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1);

    }
}
