using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string QuestionText;
    public QuestionType Type;
    public int Difficulty;
    public string[] Localizations;
    
    public string GetLocalizedText()
    {
        int languageIndex = (int)LocalizationManager.Instance.CurrentLanguage;
        return Localizations[languageIndex];
    }
} 