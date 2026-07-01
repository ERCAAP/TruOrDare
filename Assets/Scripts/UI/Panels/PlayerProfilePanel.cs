using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerProfilePanel : BasePanel
{
    [Header("Profile Info")]
    [SerializeField] private Image avatarImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerIdText;
    [SerializeField] private Button editAvatarButton;
    [SerializeField] private Button editNameButton;
    
    [Header("Statistics")]
    [SerializeField] private TextMeshProUGUI gamesPlayedText;
    [SerializeField] private TextMeshProUGUI winRateText;
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI totalPointsText;
    
    [Header("Other Elements")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button achievementsButton;
    [SerializeField] private Button leaderboardButton;
    
    private PlayerData currentPlayerData;
    
    protected override void Awake()
    {
        base.Awake();
        InitializeButtons();
    }
    
    private void InitializeButtons()
    {
        closeButton.onClick.AddListener(Hide);
        editAvatarButton.onClick.AddListener(ShowAvatarSelection);
        editNameButton.onClick.AddListener(ShowNameEdit);
        achievementsButton.onClick.AddListener(ShowAchievements);
        leaderboardButton.onClick.AddListener(ShowLeaderboard);
    }
    
    public void Initialize(PlayerData playerData)
    {
        currentPlayerData = playerData;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        // Update profile info
        avatarImage.sprite = currentPlayerData.Avatar;
        playerNameText.text = currentPlayerData.PlayerName;
        playerIdText.text = $"ID: {currentPlayerData.PlayerId}";
        
        // Update statistics
        gamesPlayedText.text = currentPlayerData.GamesPlayed.ToString();
        winRateText.text = $"{(currentPlayerData.WinRate * 100):F1}%";
        highestScoreText.text = currentPlayerData.HighestScore.ToString();
        totalPointsText.text = currentPlayerData.TotalPoints.ToString();
        
        AnimateStatistics();
    }
    
    private void AnimateStatistics()
    {
        // Animate statistics from zero
        DOTween.To(() => 0, x => gamesPlayedText.text = x.ToString(), currentPlayerData.GamesPlayed, 1f);
        DOTween.To(() => 0f, x => winRateText.text = $"{(x * 100):F1}%", currentPlayerData.WinRate, 1f);
        DOTween.To(() => 0, x => highestScoreText.text = x.ToString(), currentPlayerData.HighestScore, 1f);
        DOTween.To(() => 0, x => totalPointsText.text = x.ToString(), currentPlayerData.TotalPoints, 1f);
    }
    
    private void ShowAvatarSelection()
    {
        if (!PremiumManager.Instance.CanAccessFeature(PremiumFeature.CustomAvatars))
        {
            UIManager.Instance.ShowPremiumPrompt();
            return;
        }
        
        UIManager.Instance.ShowAvatarSelectionPanel();
    }
    
    private void OnAvatarSelected(Sprite newAvatar)
    {
        currentPlayerData.Avatar = newAvatar;
        avatarImage.sprite = newAvatar;
        SavePlayerData();
    }
    
    private void ShowNameEdit()
    {
        UIManager.Instance.ShowInputDialog(
            LocalizationManager.Instance.GetTranslation("ENTER_NAME"),
            currentPlayerData.PlayerName,
            OnNameChanged
        );
    }
    
    private void OnNameChanged(string newName)
    {
        if (string.IsNullOrEmpty(newName)) return;
        
        currentPlayerData.PlayerName = newName;
        playerNameText.text = newName;
        SavePlayerData();
    }
    
    private void SavePlayerData()
    {
        PlayerDataManager.SavePlayerData(currentPlayerData);
    }
    
    private void ShowAchievements()
    {
        UIManager.Instance.ShowPanel(PanelType.Achievements);
    }
    
    private void ShowLeaderboard()
    {
        UIManager.Instance.ShowPanel(PanelType.Leaderboard);
    }
} 