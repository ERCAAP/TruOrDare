using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class QuestionPanel : BasePanel
{
    [Header("Question Elements")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Image playerAvatar;
    [SerializeField] private Image questionTypeIcon;
    
    [Header("Buttons")]
    [SerializeField] private Button trueButton;
    [SerializeField] private Button falseButton;
    [SerializeField] private Button expandButton;
    
    [Header("Progress")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private float questionDuration = 30f;
    private float currentTime;
    
    private RectTransform questionContainer;
    private Vector2 originalSize;
    private bool isExpanded = false;
    
    protected override void Awake()
    {
        base.Awake();
        questionContainer = questionText.GetComponent<RectTransform>();
        originalSize = questionContainer.sizeDelta;
        
        InitializeButtons();
    }
    
    private void InitializeButtons()
    {
        trueButton.onClick.AddListener(() => OnAnswerSelected(true));
        falseButton.onClick.AddListener(() => OnAnswerSelected(false));
        expandButton.onClick.AddListener(ToggleQuestionSize);
    }
    
    public void SetupQuestion(Player player, QuestionData question)
    {
        playerNameText.text = player.Name;
        playerAvatar.sprite = player.Avatar;
        questionText.text = question.GetLocalizedText();
        questionTypeIcon.sprite = GetQuestionTypeSprite(question.Type);
        
        // Reset timer
        currentTime = questionDuration;
        progressBar.value = 1;
        
        // Animate entrance
        AnimateQuestionEntrance();
    }
    
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            progressBar.value = currentTime / questionDuration;
            
            if (currentTime <= 0)
            {
                OnTimeUp();
            }
        }
    }
    
    private void AnimateQuestionEntrance()
    {
        questionText.transform.localScale = Vector3.zero;
        questionText.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        
        // Animate player info
        playerAvatar.transform.localScale = Vector3.zero;
        playerNameText.alpha = 0;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(playerAvatar.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack))
                .Join(playerNameText.DOFade(1, 0.3f));
    }
    
    private void ToggleQuestionSize()
    {
        isExpanded = !isExpanded;
        Vector2 targetSize = isExpanded ? new Vector2(Screen.width, Screen.height) : originalSize;
        
        questionContainer.DOSizeDelta(targetSize, 0.3f).SetEase(Ease.OutQuad);
        expandButton.transform.DORotate(new Vector3(0, 0, isExpanded ? 180 : 0), 0.3f);
    }
    
    private void OnAnswerSelected(bool completed)
    {
        AudioManager.Instance.PlaySound("button_click");
        GameManager.Instance.CurrentGameMode.ProcessAnswer(completed);
        Hide();
    }
    
    private void OnTimeUp()
    {
        AudioManager.Instance.PlaySound("time_up");
        GameManager.Instance.CurrentGameMode.ProcessAnswer(false);
        Hide();
    }
    
    private Sprite GetQuestionTypeSprite(QuestionType type)
    {
        return Resources.Load<Sprite>($"Icons/{type.ToString().ToLower()}_icon");
    }
    
    public override void Hide()
    {
        if (isExpanded)
            ToggleQuestionSize();
            
        base.Hide();
    }
} 