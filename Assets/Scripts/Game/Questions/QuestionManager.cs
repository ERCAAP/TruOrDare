using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get; private set; }
    
    [SerializeField] private QuestionSet defaultQuestions;
    
    private List<QuestionData> truthQuestions;
    private List<QuestionData> dareQuestions;
    
    private void Awake()
    {
        Instance = this;
        LoadDefaultQuestions();
    }
    
    public async Task LoadQuestions()
    {
        // Offline modda direkt default soruları yükle
        LoadDefaultQuestions();
        await Task.CompletedTask; // Async metod için
    }
    
    private void LoadDefaultQuestions()
    {
        truthQuestions = new List<QuestionData>(defaultQuestions.TruthQuestions);
        dareQuestions = new List<QuestionData>(defaultQuestions.DareQuestions);
    }
    
    public QuestionData GetQuestion(QuestionType type)
    {
        var questions = type == QuestionType.Truth ? truthQuestions : dareQuestions;
        int index = Random.Range(0, questions.Count);
        return questions[index];
    }
}

public enum QuestionType
{
    Truth,
    Dare
} 