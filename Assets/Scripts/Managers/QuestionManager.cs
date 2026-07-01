using UnityEngine;
using System.Collections.Generic;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get; private set; }

    [SerializeField] private List<QuestionSet> questionSets;
    [SerializeField] private QuestionSet defaultSet;

    private Dictionary<QuestionType, List<QuestionData>> availableQuestions;

    private void Awake()
    {
        Instance = this;
        InitializeQuestions();
    }

    private void InitializeQuestions()
    {
        availableQuestions = new Dictionary<QuestionType, List<QuestionData>>();
        availableQuestions[QuestionType.Truth] = new List<QuestionData>();
        availableQuestions[QuestionType.Dare] = new List<QuestionData>();

        foreach (var set in questionSets)
        {
            if (!set.isPremium || PremiumManager.Instance.IsPremium)
            {
                LoadQuestionsFromSet(set);
            }
        }
    }

    private void LoadQuestionsFromSet(QuestionSet set)
    {
        foreach (var question in set.questions)
        {
            availableQuestions[question.type].Add(question);
        }
    }

    public QuestionData GetQuestion(QuestionType type)
    {
        var questions = availableQuestions[type];
        if (questions.Count == 0)
        {
            return null;
        }

        int index = Random.Range(0, questions.Count);
        return questions[index];
    }
} 