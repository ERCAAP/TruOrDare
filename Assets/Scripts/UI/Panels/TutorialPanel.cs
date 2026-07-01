using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialPanel : BasePanel
{
    [System.Serializable]
    public class TutorialStep
    {
        public string titleKey;
        public string descriptionKey;
        public GameObject highlightObject;
        public Vector2 pointerPosition;
    }
    
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private RectTransform pointer;
    [SerializeField] private Image overlay;
    
    [Header("Tutorial Data")]
    [SerializeField] private TutorialStep[] tutorialSteps;
    
    private int currentStepIndex = 0;
    
    protected override void Awake()
    {
        base.Awake();
        InitializeButtons();
    }
    
    private void InitializeButtons()
    {
        nextButton.onClick.AddListener(OnNextClicked);
        skipButton.onClick.AddListener(CompleteTutorial);
    }
    
    public override void Show()
    {
        base.Show();
        currentStepIndex = 0;
        ShowCurrentStep();
    }
    
    private void ShowCurrentStep()
    {
        var step = tutorialSteps[currentStepIndex];
        
        // Update texts
        titleText.text = LocalizationManager.Instance.GetTranslation(step.titleKey);
        descriptionText.text = LocalizationManager.Instance.GetTranslation(step.descriptionKey);
        
        // Update button text
        bool isLastStep = currentStepIndex == tutorialSteps.Length - 1;
        nextButton.GetComponentInChildren<TextMeshProUGUI>().text = 
            LocalizationManager.Instance.GetTranslation(isLastStep ? "FINISH" : "NEXT");
        
        // Highlight current element
        HighlightElement(step.highlightObject);
        
        // Move pointer
        AnimatePointer(step.pointerPosition);
    }
    
    private void HighlightElement(GameObject target)
    {
        // Fade overlay except for highlighted area
        overlay.DOFade(0.8f, 0.3f);
        
        if (target != null)
        {
            RectTransform targetRect = target.GetComponent<RectTransform>();
            targetRect.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
    
    private void AnimatePointer(Vector2 targetPosition)
    {
        pointer.DOAnchorPos(targetPosition, 0.5f).SetEase(Ease.OutBack);
        pointer.DORotate(new Vector3(0, 0, 10), 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }
    
    private void OnNextClicked()
    {
        AudioManager.Instance.PlaySound("button_click");
        
        currentStepIndex++;
        if (currentStepIndex >= tutorialSteps.Length)
        {
            CompleteTutorial();
        }
        else
        {
            ShowCurrentStep();
        }
    }
    
    private void CompleteTutorial()
    {
        PlayerPrefs.SetInt("TutorialCompleted", 1);
        PlayerPrefs.Save();
        
        Hide();
    }
    
    public override void Hide()
    {
        // Reset all highlights
        foreach (var step in tutorialSteps)
        {
            if (step.highlightObject != null)
            {
                step.highlightObject.transform.DOKill();
                step.highlightObject.transform.localScale = Vector3.one;
            }
        }
        
        overlay.DOFade(0, 0.3f);
        base.Hide();
    }
} 