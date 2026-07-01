using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LeaderboardItemUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image rankBadge;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image playerAvatar;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject highlightEffect;
    [SerializeField] private GameObject currentPlayerIndicator;
    
    [Header("Visual Settings")]
    [SerializeField] private Color[] rankColors; // Colors for top ranks
    [SerializeField] private Sprite[] rankBadges; // Different badge sprites for top ranks
    
    private PlayerData playerData;
    
    public void Initialize(PlayerData player, int rank)
    {
        playerData = player;
        UpdateUI(rank);
    }
    
    private void UpdateUI(int rank)
    {
        if (playerData == null) return;
        
        playerAvatar.sprite = playerData.Avatar;
        playerNameText.text = playerData.PlayerName;
        scoreText.text = playerData.Points.ToString();
        rankText.text = $"#{rank}";
        
        bool isCurrentPlayer = playerData.PlayerId == PlayerDataManager.Instance.CurrentPlayerId;
        currentPlayerIndicator.SetActive(isCurrentPlayer);
    }
    
    public void SetupItem(Player player, int rank, Color rankColor)
    {
        // Setup rank
        rankText.text = $"#{rank}";
        rankBadge.color = rank <= rankColors.Length ? rankColors[rank - 1] : rankColor;
        
        if (rank <= rankBadges.Length)
        {
            rankBadge.sprite = rankBadges[rank - 1];
        }
        
        // Setup player info
        playerAvatar.sprite = player.Avatar;
        playerNameText.text = player.Name;
        scoreText.text = player.Points.ToString();
        
        // Highlight if it's the current player
        bool isCurrentPlayer = player.PlayerId == PlayerDataManager.Instance.CurrentPlayerId;
        highlightEffect.SetActive(isCurrentPlayer);
        
        if (isCurrentPlayer)
        {
            AnimateHighlight();
        }
    }
    
    private void AnimateHighlight()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(highlightEffect.transform.DOScale(1.1f, 0.5f))
                .Append(highlightEffect.transform.DOScale(1f, 0.5f))
                .SetLoops(-1);
    }
    
    public void AnimateScoreChange(int newScore)
    {
        int currentScore = int.Parse(scoreText.text);
        
        DOTween.To(() => currentScore, x => {
            currentScore = x;
            scoreText.text = x.ToString();
        }, newScore, 0.5f).SetEase(Ease.OutQuad);
        
        transform.DOPunchScale(Vector3.one * 0.1f, 0.3f);
    }
    
    public void UpdateScore()
    {
        scoreText.text = playerData.Points.ToString();
    }
} 