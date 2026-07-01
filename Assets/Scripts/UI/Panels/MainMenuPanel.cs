using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuPanel : BasePanel
{
    [Header("Main Elements")]
    [SerializeField] private RectTransform titleText;
    [SerializeField] private RectTransform stickerContainer;
    [SerializeField] private Button friendsModeButton;
    [SerializeField] private Button couplesModeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private PlayerListView playerListView;
    
    [Header("Animation Settings")]
    [SerializeField] private float titlePunchScale = 1.1f;
    [SerializeField] private float stickerRotation = 10f;
    
    [SerializeField] private GameObject[] panels;
    
    protected override void Awake()
    {
        base.Awake();
        InitializeButtons();
    }
    
    private void OnEnable()
    {
        StartAnimations();
    }
    
    private void InitializeButtons()
    {
        friendsModeButton.onClick.AddListener(() => OnModeSelected(GameModeType.Friends));
        couplesModeButton.onClick.AddListener(() => OnModeSelected(GameModeType.Couples));
        settingsButton.onClick.AddListener(OpenSettings);
    }
    
    private void StartAnimations()
    {
        // Title animation
        titleText.DOScale(titlePunchScale, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
            
        // Sticker animation
        stickerContainer.DORotate(new Vector3(0, 0, stickerRotation), 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }
    
    private void OnModeSelected(GameModeType modeType)
    {
        if (!PremiumManager.Instance.CanAccessGameMode(modeType))
        {
            UIManager.Instance.ShowPremiumPrompt();
            return;
        }
        
        if (playerListView.PlayerCount < GameManager.Instance.MinPlayers)
        {
            UIManager.Instance.ShowMessage(LocalizationManager.Instance.GetTranslation("NOT_ENOUGH_PLAYERS"));
            return;
        }
        
        GameManager.Instance.StartGame(modeType);
    }
    
    private void OpenSettings()
    {
        UIManager.Instance.ShowPanel(PanelType.Settings);
    }
    
    public override void Hide()
    {
        DOTween.Kill(titleText);
        DOTween.Kill(stickerContainer);
        base.Hide();
    }

    private void ShowPanel(GameObject panel)
    {
        // ... panel gösterme mantığı
    }
} 