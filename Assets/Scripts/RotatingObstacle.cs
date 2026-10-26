using DG.Tweening;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{

    [SerializeField] private float duration = 2f;

    void Start()
    { 
            transform.DOLocalRotate(new Vector3(0,360,0), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        
    }

    
}
