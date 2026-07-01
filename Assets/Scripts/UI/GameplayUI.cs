using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameplayUI : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private Image playerAvatar;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("Question Display")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Image questionTypeIcon;
    
    [Header("Timer")]
    [SerializeField] private Image timerFill;
    [SerializeField] private TextMeshProUGUI timerText;
    
    [Header("Answer Buttons")]
    [SerializeField] private Button completedButton;
    [SerializeField] private Button failedButton;
    
    [Header("Animations")]
    [SerializeField] private float questionRevealDuration = 0.5f;
    [SerializeField] private CanvasGroup questionGroup;

    private void Awake()
    {
        SetupButtons();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SetupButtons()
    {
        completedButton.onClick.AddListener(() => OnAnswerSubmitted(true));
        failedButton.onClick.AddListener(() => OnAnswerSubmitted(false));
    }

    private void SubscribeToEvents()
    {
        GameEvents.OnNewRoundStarted += OnNewRound;
        GameEvents.OnPlayerScoreChanged += UpdatePlayerScore;
    }

    private void UnsubscribeFromEvents()
    {
        GameEvents.OnNewRoundStarted -= OnNewRound;
        GameEvents.OnPlayerScoreChanged -= UpdatePlayerScore;
    }

    private void OnNewRound(PlayerData player, QuestionData question)
    {
        UpdatePlayerInfo(player);
        ShowQuestion(question);
        ResetTimer();
    }

    private void UpdatePlayerInfo(PlayerData player)
    {
        playerNameText.text = player.Name;
        playerAvatar.sprite = AvatarManager.Instance.GetAvatar(player.Gender, player.AvatarIndex);
        scoreText.text = $"Score: {player.Points}";
    }

    private void ShowQuestion(QuestionData question)
    {
        questionText.text = question.questionText;
        questionGroup.alpha = 0;
        questionGroup.DOFade(1, questionRevealDuration);
    }

    private void UpdatePlayerScore(PlayerData player)
    {
        if (playerNameText.text == player.Name)
        {
            scoreText.text = $"Score: {player.Points}";
        }
    }

    private void OnAnswerSubmitted(bool completed)
    {
        GameFlowManager.Instance.SubmitAnswer(completed);
    }

    private void ResetTimer()
    {
        timerFill.fillAmount = 1;
        // Start timer animation
        timerFill.DOFillAmount(0, GameManager.Instance.gameSettings.playerTurnTime)
            .SetEase(Ease.Linear);
    }
} 