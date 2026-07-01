using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }
    
    public event System.Action<Achievement> OnAchievementUnlocked;
    public event System.Action<Achievement, float> OnProgressUpdated;

    private void Awake()
    {
        Instance = this;
    }

    public List<Achievement> GetAllAchievements()
    {
        // TODO: Implement achievement retrieval
        return new List<Achievement>();
    }

    public List<Achievement> GetUnlockedAchievements()
    {
        // TODO: Implement unlocked achievement retrieval
        return new List<Achievement>();
    }

    public void UpdateAchievements()
    {
        // TODO: Implement achievement update logic
    }
} 