using UnityEngine;
using DG.Tweening;

public class UIAnimationController : MonoBehaviour
{
    [SerializeField] private float defaultDuration = 0.3f;
    [SerializeField] private Ease defaultEase = Ease.OutBack;

    public void AnimateScale(Transform target, Vector3 endValue, float duration = -1)
    {
        target.DOScale(endValue, duration < 0 ? defaultDuration : duration)
            .SetEase(defaultEase);
    }

    public void AnimateMove(Transform target, Vector3 endValue, float duration = -1)
    {
        target.DOMove(endValue, duration < 0 ? defaultDuration : duration)
            .SetEase(defaultEase);
    }

    public void AnimateRotate(Transform target, Vector3 endValue, float duration = -1)
    {
        target.DORotate(endValue, duration < 0 ? defaultDuration : duration)
            .SetEase(defaultEase);
    }

    public void AnimateFade(CanvasGroup target, float endValue, float duration = -1)
    {
        target.DOFade(endValue, duration < 0 ? defaultDuration : duration)
            .SetEase(defaultEase);
    }
} 