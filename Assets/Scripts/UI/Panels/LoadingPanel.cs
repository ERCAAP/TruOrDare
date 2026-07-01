using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoadingPanel : BasePanel
{
    [Header("Loading Elements")]
    [SerializeField] private RectTransform loadingIcon;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private TextMeshProUGUI tipText;
    [SerializeField] private Image progressBar;
    
    [Header("Animation Settings")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float tipChangeDuration = 3f;
    
    private string[] loadingTips;
    private float tipTimer;
    private int currentTipIndex;
    
    protected override void Awake()
    {
        base.Awake();
        InitializeTips();
    }
    
    private void InitializeTips()
    {
        loadingTips = new[]
        {
            LocalizationManager.Instance.GetTranslation("LOADING_TIP_1"),
            LocalizationManager.Instance.GetTranslation("LOADING_TIP_2"),
            LocalizationManager.Instance.GetTranslation("LOADING_TIP_3"),
            LocalizationManager.Instance.GetTranslation("LOADING_TIP_4")
        };
        
        ShowRandomTip();
    }
    
    private void Update()
    {
        // Rotate loading icon
        loadingIcon.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        
        // Update tips
        tipTimer += Time.deltaTime;
        if (tipTimer >= tipChangeDuration)
        {
            tipTimer = 0;
            ShowNextTip();
        }
    }
    
    public void SetProgress(float progress)
    {
        progressBar.DOFillAmount(progress, 0.2f);
        
        if (progress >= 1)
        {
            loadingText.text = LocalizationManager.Instance.GetTranslation("LOADING_COMPLETE");
        }
    }
    
    private void ShowNextTip()
    {
        currentTipIndex = (currentTipIndex + 1) % loadingTips.Length;
        AnimateTipChange(loadingTips[currentTipIndex]);
    }
    
    private void ShowRandomTip()
    {
        currentTipIndex = Random.Range(0, loadingTips.Length);
        tipText.text = loadingTips[currentTipIndex];
    }
    
    private void AnimateTipChange(string newTip)
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(tipText.DOFade(0, 0.3f))
                .AppendCallback(() => tipText.text = newTip)
                .Append(tipText.DOFade(1, 0.3f));
    }
    
    public override void Show()
    {
        base.Show();
        
        // Reset and start animations
        loadingIcon.rotation = Quaternion.identity;
        progressBar.fillAmount = 0;
        tipTimer = 0;
        ShowRandomTip();
        
        // Animate entrance
        loadingIcon.localScale = Vector3.zero;
        loadingIcon.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    
    public override void Hide()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(loadingIcon.DOScale(0, 0.3f))
                .Join(progressBar.DOFade(0, 0.3f))
                .Join(tipText.DOFade(0, 0.3f))
                .OnComplete(() => base.Hide());
    }
} 