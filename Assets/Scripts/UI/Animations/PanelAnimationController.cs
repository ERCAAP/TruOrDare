using UnityEngine;
using DG.Tweening;

public class PanelAnimationController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;
    
    [Header("Animation Settings")]
    [SerializeField] private float showDuration = 0.3f;
    [SerializeField] private float hideDuration = 0.2f;
    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;
    [SerializeField] private Vector2 showScale = Vector2.one;
    [SerializeField] private Vector2 hideScale = Vector2.zero;
    
    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        rectTransform.localScale = hideScale;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Join(canvasGroup.DOFade(1, showDuration));
        sequence.Join(rectTransform.DOScale(showScale, showDuration).SetEase(showEase));
    }
    
    public void Hide()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(canvasGroup.DOFade(0, hideDuration));
        sequence.Join(rectTransform.DOScale(hideScale, hideDuration).SetEase(hideEase));
        sequence.OnComplete(() => gameObject.SetActive(false));
    }
} 