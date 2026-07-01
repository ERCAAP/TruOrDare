using UnityEngine;
using DG.Tweening;

public abstract class BasePanel : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float animationDuration = 0.3f;
    
    protected virtual void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public virtual void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        transform.localScale = Vector3.one * 0.9f;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1, animationDuration))
                .Join(transform.DOScale(1, animationDuration).SetEase(Ease.OutBack));
    }
    
    public virtual void Hide()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(0, animationDuration))
                .Join(transform.DOScale(0.9f, animationDuration))
                .OnComplete(() => gameObject.SetActive(false));
    }

    protected virtual void OnEnable()
    {
        // Base implementation
    }

    protected virtual void OnDisable()
    {
        // Base implementation
    }
} 