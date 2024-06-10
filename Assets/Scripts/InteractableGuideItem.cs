using DG.Tweening;
using UnityEngine;

public class InteractableGuideItem : MonoBehaviour
{
    void Start()
    {
        Vector3 initScale = transform.localScale;
        Vector3 totalScale = initScale * 1.1f;
        transform.DOScale(totalScale, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    
}
