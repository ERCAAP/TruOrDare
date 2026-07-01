using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class LeaderboardPanel : BasePanel
{
    [Header("UI Elements")]
    [SerializeField] private Transform podiumContainer;
    [SerializeField] private LeaderboardItemUI[] podiumItems; // Top 3 players
    [SerializeField] private Transform leaderboardContainer;
    [SerializeField] private GameObject leaderboardItemPrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI titleText;
    
    [Header("Visual Settings")]
    [SerializeField] private Color[] rankColors; // Colors for 1st, 2nd, 3rd place
    [SerializeField] private float itemSpacing = 10f;
    
    private List<LeaderboardItemUI> leaderboardItems = new List<LeaderboardItemUI>();
    
    protected override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(Hide);
    }
    
    public void UpdateLeaderboard(List<Player> players)
    {
        ClearLeaderboard();
        
        var sortedPlayers = players.OrderByDescending(p => p.Points).ToList();
        
        // Update podium (top 3)
        for (int i = 0; i < Mathf.Min(3, sortedPlayers.Count); i++)
        {
            podiumItems[i].gameObject.SetActive(true);
            podiumItems[i].SetupItem(sortedPlayers[i], i + 1, rankColors[i]);
        }
        
        // Update rest of the leaderboard
        for (int i = 3; i < sortedPlayers.Count; i++)
        {
            CreateLeaderboardItem(sortedPlayers[i], i + 1);
        }
        
        AnimateLeaderboard();
    }
    
    private void CreateLeaderboardItem(Player player, int rank)
    {
        GameObject itemGO = Instantiate(leaderboardItemPrefab, leaderboardContainer);
        LeaderboardItemUI item = itemGO.GetComponent<LeaderboardItemUI>();
        
        item.SetupItem(player, rank, Color.white);
        leaderboardItems.Add(item);
    }
    
    private void ClearLeaderboard()
    {
        foreach (var item in leaderboardItems)
        {
            Destroy(item.gameObject);
        }
        leaderboardItems.Clear();
        
        // Reset podium
        foreach (var podiumItem in podiumItems)
        {
            podiumItem.gameObject.SetActive(false);
        }
    }
    
    private void AnimateLeaderboard()
    {
        // Animate title
        titleText.transform.localScale = Vector3.zero;
        titleText.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        
        // Animate podium
        podiumContainer.localScale = Vector3.zero;
        podiumContainer.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetDelay(0.3f);
        
        // Animate leaderboard items
        for (int i = 0; i < leaderboardItems.Count; i++)
        {
            var item = leaderboardItems[i];
            item.transform.localScale = Vector3.zero;
            item.transform.DOScale(1, 0.3f)
                .SetEase(Ease.OutBack)
                .SetDelay(0.5f + (i * 0.1f));
        }
    }
    
    public override void Hide()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(titleText.transform.DOScale(0, 0.3f))
                .Join(podiumContainer.DOScale(0, 0.3f));
                
        foreach (var item in leaderboardItems)
        {
            sequence.Join(item.transform.DOScale(0, 0.3f));
        }
        
        sequence.OnComplete(() => base.Hide());
    }
} 