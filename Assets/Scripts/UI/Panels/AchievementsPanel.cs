using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class AchievementsPanel : BasePanel
{
    [Header("UI Elements")]
    [SerializeField] private Transform achievementsContainer;
    [SerializeField] private GameObject achievementItemPrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private ParticleSystem unlockEffect;
    
    private List<AchievementItemUI> achievementItems = new List<AchievementItemUI>();
    private Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
    
    protected override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(Hide);
    }
    
    public override void Show()
    {
        base.Show();
        LoadAchievements();
        
        // Subscribe to events
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnAchievementUnlocked += OnAchievementUnlocked;
            AchievementManager.Instance.OnProgressUpdated += OnProgressUpdated;
        }
    }
    
    public override void Hide()
    {
        base.Hide();
        
        // Unsubscribe from events
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnAchievementUnlocked -= OnAchievementUnlocked;
            AchievementManager.Instance.OnProgressUpdated -= OnProgressUpdated;
        }
    }
    
    private void LoadAchievements()
    {
        var achievementList = AchievementManager.Instance.GetAllAchievements();
        achievements.Clear();
        foreach (var achievement in achievementList)
        {
            achievements[achievement.Id] = achievement;
            CreateAchievementItem(achievement);
        }
        
        AnimateItems();
    }
    
    private void CreateAchievementItem(Achievement achievement)
    {
        GameObject itemGO = Instantiate(achievementItemPrefab, achievementsContainer);
        AchievementItemUI item = itemGO.GetComponent<AchievementItemUI>();
        
        item.Initialize(achievement);
        achievementItems.Add(item);
    }
    
    private void AnimateItems()
    {
        for (int i = 0; i < achievementItems.Count; i++)
        {
            var item = achievementItems[i];
            item.transform.localScale = Vector3.zero;
            
            item.transform.DOScale(1, 0.3f)
                .SetDelay(i * 0.1f)
                .SetEase(Ease.OutBack);
        }
    }
    
    private void OnAchievementUnlocked(Achievement achievement)
    {
        if (achievements.TryGetValue(achievement.Id, out var existingAchievement))
        {
            existingAchievement.IsUnlocked = true;
            var item = achievementItems.Find(x => x.AchievementId == achievement.Id);
            if (item != null)
            {
                item.UpdateUnlocked();
                PlayUnlockEffect(item.transform.position);
            }
        }
    }
    
    private void OnProgressUpdated(Achievement achievement, float progress)
    {
        if (achievements.TryGetValue(achievement.Id, out var existingAchievement))
        {
            existingAchievement.Progress = progress;
            var item = achievementItems.Find(x => x.AchievementId == achievement.Id);
            item?.UpdateProgress(progress);
        }
    }
    
    private void PlayUnlockEffect(Vector3 position)
    {
        unlockEffect.transform.position = position;
        unlockEffect.Play();
        AudioManager.Instance.PlaySound("achievement_unlocked");
    }
} 