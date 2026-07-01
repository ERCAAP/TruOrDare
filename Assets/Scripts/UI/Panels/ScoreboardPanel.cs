using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class ScoreboardPanel : BasePanel
{
    [Header("Scoreboard Elements")]
    [SerializeField] private Transform scoreContainer;
    [SerializeField] private ScoreItemUI scoreItemPrefab;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI roundText;
    
    private Dictionary<string, ScoreItemUI> scoreItems = new Dictionary<string, ScoreItemUI>();
    
    protected override void OnEnable()
    {
        base.OnEnable();
        GameEvents.OnPlayerScoreChanged += UpdatePlayerScore;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        GameEvents.OnPlayerScoreChanged -= UpdatePlayerScore;
    }
    
    public void Initialize(List<PlayerData> players)
    {
        ClearScoreItems();
        foreach (var player in players)
        {
            CreateScoreItem(player);
        }
    }
    
    private void CreateScoreItem(PlayerData player)
    {
        var item = Instantiate(scoreItemPrefab, scoreContainer);
        item.Initialize(player);
        scoreItems[player.PlayerId] = item;
    }
    
    private void UpdatePlayerScore(PlayerData player)
    {
        if (scoreItems.TryGetValue(player.PlayerId, out var item))
        {
            item.UpdateScore(player.Points);
        }
    }
    
    private void SortScoreboard()
    {
        var sortedItems = scoreItems.Values.OrderByDescending(item => item.CurrentScore).ToList();
        
        for (int i = 0; i < sortedItems.Count; i++)
        {
            sortedItems[i].transform.SetSiblingIndex(i);
            sortedItems[i].UpdateRank(i + 1);
        }
    }
    
    public void UpdateProgress(float progress)
    {
        progressBar.value = progress;
    }
    
    public void SetRound(int current, int total)
    {
        roundText.text = $"{LocalizationManager.Instance.GetTranslation("ROUND")} {current}/{total}";
    }
    
    private void ClearScoreItems()
    {
        foreach (var item in scoreItems.Values)
        {
            Destroy(item.gameObject);
        }
        scoreItems.Clear();
    }
} 