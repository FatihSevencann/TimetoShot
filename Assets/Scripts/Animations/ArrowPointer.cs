
using UnityEngine;
using DG.Tweening;

public class ArrowPointer : MonoBehaviour
{

    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(minValue, 1f));
        sequence.Append(transform.DOLocalMoveY(maxValue, 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
    
}
