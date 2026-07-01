using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string questionText;
    public QuestionType type;
    public DifficultyLevel difficulty;
    public bool isPremium;
    public string[] localizedTexts; // Her dil için soru metni
}

public enum QuestionType
{
    Truth,
    Dare
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
} 