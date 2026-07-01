using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestionSet", menuName = "TruthOrDare/QuestionSet")]
public class QuestionSet : ScriptableObject
{
    public string setName;
    public bool isPremium;
    public DifficultyLevel difficulty;
    public List<QuestionData> questions;
    
    [Header("Localization")]
    public Language defaultLanguage = Language.English;
    
    [Header("Age Rating")]
    public int minimumAge = 13;
    
    public QuestionData GetRandomQuestion(QuestionType type)
    {
        var typeQuestions = questions.FindAll(q => q.type == type);
        if (typeQuestions.Count == 0) return null;
        
        return typeQuestions[Random.Range(0, typeQuestions.Count)];
    }
} 