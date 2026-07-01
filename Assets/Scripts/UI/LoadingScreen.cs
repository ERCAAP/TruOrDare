using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private CanvasGroup canvasGroup;
    
    public void Show(float duration = 0.5f)
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, duration);
    }
    
    public void Hide(float duration = 0.5f)
    {
        canvasGroup.DOFade(0, duration)
            .OnComplete(() => gameObject.SetActive(false));
    }
    
    public void UpdateProgress(float progress)
    {
        progressBar.fillAmount = progress;
        progressText.text = $"{(progress * 100):0}%";
    }
} 