using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class GameOverPanel : BasePanel
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private RectTransform winnerContainer;
    [SerializeField] private Image winnerAvatar;
    [SerializeField] private TextMeshProUGUI winnerNameText;
    [SerializeField] private TextMeshProUGUI winnerScoreText;
    [SerializeField] private ParticleSystem confettiEffect;
    
    [Header("Buttons")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button shareButton;
    
    [Header("Animation Settings")]
    [SerializeField] private float entranceDelay = 0.5f;
    [SerializeField] private float elementDelay = 0.2f;
    
    protected override void Awake()
    {
        base.Awake();
        InitializeButtons();
    }
    
    private void InitializeButtons()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        shareButton.onClick.AddListener(OnShareClicked);
    }
    
    public void ShowResults(Player winner, Player[] allPlayers)
    {
        // Setup winner info
        winnerAvatar.sprite = winner.Avatar;
        winnerNameText.text = winner.Name;
        winnerScoreText.text = winner.Points.ToString();
        
        Show();
        
        // Play effects
        if (confettiEffect != null)
        {
            confettiEffect.Play();
        }
        
        AudioManager.Instance.PlaySound("game_over_win");
    }
    
    public override void Show()
    {
        base.Show();
        
        // Reset scales
        titleText.transform.localScale = Vector3.zero;
        winnerContainer.localScale = Vector3.zero;
        playAgainButton.transform.localScale = Vector3.zero;
        mainMenuButton.transform.localScale = Vector3.zero;
        shareButton.transform.localScale = Vector3.zero;
        
        // Animate elements
        Sequence sequence = DOTween.Sequence();
        
        sequence.AppendInterval(entranceDelay)
                .Append(titleText.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack))
                .Append(winnerContainer.DOScale(1, 0.5f).SetEase(Ease.OutBack))
                .AppendInterval(elementDelay)
                .Append(playAgainButton.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack))
                .Append(mainMenuButton.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack))
                .Append(shareButton.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
    }
    
    private void OnPlayAgainClicked()
    {
        AudioManager.Instance.PlaySound("button_click");
        GameManager.Instance.RestartGame();
        Hide();
    }
    
    private void OnMainMenuClicked()
    {
        AudioManager.Instance.PlaySound("button_click");
        GameManager.Instance.ReturnToMainMenu();
        Hide();
    }
    
    private void OnShareClicked()
    {
        AudioManager.Instance.PlaySound("button_click");
        ShareGameResults();
    }
    
    private void ShareGameResults()
    {
        string shareText = string.Format(
            LocalizationManager.Instance.GetTranslation("SHARE_TEXT"),
            winnerNameText.text,
            winnerScoreText.text
        );
        
        #if UNITY_ANDROID || UNITY_IOS
        new NativeShare()
            .SetSubject(LocalizationManager.Instance.GetTranslation("SHARE_SUBJECT"))
            .SetText(shareText)
            .Share();
        #endif
    }
    
    public override void Hide()
    {
        if (confettiEffect != null)
        {
            confettiEffect.Stop();
        }
        
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(winnerContainer.DOScale(0, 0.3f))
                .Join(titleText.transform.DOScale(0, 0.3f))
                .Join(playAgainButton.transform.DOScale(0, 0.2f))
                .Join(mainMenuButton.transform.DOScale(0, 0.2f))
                .Join(shareButton.transform.DOScale(0, 0.2f))
                .OnComplete(() => base.Hide());
    }
} 