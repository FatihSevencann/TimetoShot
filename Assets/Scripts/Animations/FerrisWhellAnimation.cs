using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FerrisWhellAnimation : MonoBehaviour
{
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private List<GameObject> ferrisChild;

    private void Start()
    {
        transform.DOLookAt(rotationPoint.position, rotateSpeed, AxisConstraint.None);

        transform.DORotate(new Vector3(0f, 0f, 360f), rotateSpeed, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1);


    }

    private void Update()
    {
        foreach (var child in ferrisChild)
        {
            var rot = child.transform.eulerAngles;
            rot.z = 0;
            child.transform.eulerAngles = rot;
        }
    }
}
