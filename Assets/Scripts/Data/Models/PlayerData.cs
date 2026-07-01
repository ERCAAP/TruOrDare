using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public int Points { get; private set; }
    public int AvatarIndex { get; set; }

    public PlayerData(string name, Gender gender, int avatarIndex = 0)
    {
        Name = name;
        Gender = gender;
        AvatarIndex = avatarIndex;
        Points = 0;
    }

    public void AddPoints(int points)
    {
        Points += points;
        GameEvents.OnPlayerScoreChanged?.Invoke(this);
    }

    public void ResetPoints()
    {
        Points = 0;
        GameEvents.OnPlayerScoreChanged?.Invoke(this);
    }
}

public enum Gender
{
    Male,
    Female
} 