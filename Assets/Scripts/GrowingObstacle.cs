using DG.Tweening;
using System;
using UnityEngine;

public class GrowingObstacle : MonoBehaviour
{

    [SerializeField] private float duration = 2f;
    [SerializeField] private Vector3 size = new Vector3(5,5,5);
    void Start()
    {
        transform.DOScale(transform.localScale + size, duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1,LoopType.Yoyo);
    }

    
}
