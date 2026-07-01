using UnityEngine;
using DG.Tweening;

public class UITransitionManager : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;
    
    public void ShowPanel(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true);
        panel.alpha = 0;
        panel.transform.localScale = Vector3.zero;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Join(panel.DOFade(1, transitionDuration));
        sequence.Join(panel.transform.DOScale(1, transitionDuration).SetEase(showEase));
    }
    
    public void HidePanel(CanvasGroup panel)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(panel.DOFade(0, transitionDuration));
        sequence.Join(panel.transform.DOScale(0, transitionDuration).SetEase(hideEase));
        sequence.OnComplete(() => panel.gameObject.SetActive(false));
    }
} 