using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button friendsModeButton;
    [SerializeField] private Button couplesModeButton;
    [SerializeField] private Button settingsButton;
    
    [Header("UI Elements")]
    [SerializeField] private PlayerListManager playerListManager;
    [SerializeField] private RectTransform titleText;
    [SerializeField] private RectTransform stickerContainer;
    
    [SerializeField] private GameObject premiumPromptPanel;
    [SerializeField] private GameObject settingsPanel;
    
    private void Start()
    {
        InitializeUI();
        AnimateUI();
    }
    
    private void InitializeUI()
    {
        friendsModeButton.onClick.AddListener(() => OnGameModeSelected(GameModeType.Friends));
        couplesModeButton.onClick.AddListener(() => OnGameModeSelected(GameModeType.Couples));
        settingsButton.onClick.AddListener(OpenSettings);
    }
    
    private void AnimateUI()
    {
        titleText.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo);
        stickerContainer.DORotate(new Vector3(0, 0, 10), 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }
    
    private void OnGameModeSelected(GameModeType modeType)
    {
        if (!PremiumManager.Instance.CanAccessGameMode(modeType))
        {
            ShowPremiumPrompt();
            return;
        }
        
        GameManager.Instance.StartGame(modeType);
    }
    
    private void ShowPremiumPrompt()
    {
        UIManager.Instance.ShowPanel(premiumPromptPanel);
    }
    
    private void OpenSettings()
    {
        UIManager.Instance.ShowPanel(settingsPanel);
    }
} 