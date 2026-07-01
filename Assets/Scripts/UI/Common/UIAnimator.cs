using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    public static void AnimateSticker(RectTransform sticker, float duration = 1f)
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(sticker.DOScale(1.2f, duration/2))
                .Append(sticker.DOScale(1f, duration/2))
                .SetLoops(-1);
    }
    
    public static void ShowPanel(GameObject panel)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = panel.AddComponent<CanvasGroup>();
        
        panel.SetActive(true);
        canvasGroup.alpha = 0;
        
        canvasGroup.DOFade(1, 0.3f);
        panel.transform.DOScale(1, 0.3f).From(0.8f).SetEase(Ease.OutBack);
    }
} 