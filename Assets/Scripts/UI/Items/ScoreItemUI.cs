using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreItemUI : MonoBehaviour
{
    [SerializeField] private Image avatarImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Image rankBadge;
    
    public int CurrentScore { get; private set; }
    
    public void Initialize(PlayerData player)
    {
        avatarImage.sprite = player.Avatar;
        playerNameText.text = player.PlayerName;
        UpdateScore(player.Points);
    }
    
    public void UpdateScore(int newScore)
    {
        if (newScore != CurrentScore)
        {
            // Animate score change
            DOTween.To(() => CurrentScore, x => {
                CurrentScore = x;
                scoreText.text = x.ToString();
            }, newScore, 0.5f).SetEase(Ease.OutQuad);
            
            transform.DOPunchScale(Vector3.one * 1.1f, 0.3f);
        }
    }
    
    public void UpdateRank(int rank)
    {
        rankText.text = rank.ToString();
        
        // Update badge color based on rank
        Color badgeColor = rank switch
        {
            1 => Color.yellow, // Gold
            2 => Color.grey,   // Silver
            3 => new Color(0.8f, 0.5f, 0.2f), // Bronze
            _ => Color.white
        };
        
        rankBadge.color = badgeColor;
    }
} 