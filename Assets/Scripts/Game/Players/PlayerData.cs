using UnityEngine;
using System.Collections.Generic;

public class PlayerData
{
    public string PlayerId { get; private set; }
    public string UserId { get; private set; }
    public string Name { get; set; }
    public string PlayerName { get; set; }
    public string Username { get; set; }
    public Sprite Avatar { get; set; }
    
    // Game Stats
    public int Points { get; private set; }
    public int Score { get; private set; }
    public int GamesPlayed { get; private set; }
    public float WinRate { get; private set; }
    public int HighestScore { get; private set; }
    public int TotalPoints { get; private set; }
    public List<Achievement> Achievements { get; private set; }
    
    public PlayerData(string name, Sprite avatar)
    {
        PlayerId = System.Guid.NewGuid().ToString();
        UserId = System.Guid.NewGuid().ToString();
        Name = name;
        PlayerName = name;
        Username = name;
        Avatar = avatar;
        Points = 0;
        Score = 0;
        GamesPlayed = 0;
        WinRate = 0;
        HighestScore = 0;
        TotalPoints = 0;
        Achievements = new List<Achievement>();
    }
    
    public void AddPoints(int amount)
    {
        Points += amount;
        Score += amount;
        TotalPoints += amount;
        if (Score > HighestScore)
            HighestScore = Score;
        GameEvents.OnPlayerScoreChanged?.Invoke(this);
    }
    
    public void UpdateStats(bool won)
    {
        GamesPlayed++;
        if (won)
            WinRate = (WinRate * (GamesPlayed - 1) + 1) / GamesPlayed;
        else
            WinRate = (WinRate * (GamesPlayed - 1)) / GamesPlayed;
    }
} 