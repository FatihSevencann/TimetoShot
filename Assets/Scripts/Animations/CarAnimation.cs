using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarAnimation : MonoBehaviour
{
    [SerializeField] private Transform startPoint,EndPoint;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        if (EndPoint != null)
        {
            transform.position = startPoint.position;

            DOTween.Sequence()
                .Append(transform.DOMove(EndPoint.position, moveSpeed))
                .AppendCallback(() => ResetCarPosition())
                .SetLoops(-1);
        }
      
    }

    void ResetCarPosition()
    {
        transform.position = startPoint.position;
    }
    
    
    
}
