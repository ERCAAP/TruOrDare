using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PremiumPanel : BasePanel
{
    [Header("UI Elements")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button startTrialButton;
    [SerializeField] private Button[] purchaseButtons;
    [SerializeField] private TextMeshProUGUI[] priceTexts;
    [SerializeField] private RectTransform premiumIcon;
    
    [Header("Premium Info")]
    [SerializeField] private TextMeshProUGUI trialTimeText;
    [SerializeField] private GameObject trialAvailableGroup;
    [SerializeField] private GameObject premiumActiveGroup;
    
    protected override void Awake()
    {
        base.Awake();
        InitializeButtons();
    }
    
    private void OnEnable()
    {
        UpdateUI();
        AnimateElements();
    }
    
    private void InitializeButtons()
    {
        closeButton.onClick.AddListener(Hide);
        startTrialButton.onClick.AddListener(StartTrial);
        
        // Initialize purchase buttons
        for (int i = 0; i < purchaseButtons.Length; i++)
        {
            int index = i; // Capture for lambda
            purchaseButtons[i].onClick.AddListener(() => OnPurchaseSelected(index));
        }
    }
    
    private void UpdateUI()
    {
        bool isPremium = PremiumManager.Instance.IsPremium;
        bool hasTrialAvailable = PremiumManager.Instance.CanStartTrial;
        
        trialAvailableGroup.SetActive(hasTrialAvailable && !isPremium);
        premiumActiveGroup.SetActive(isPremium);
        
        if (hasTrialAvailable)
        {
            trialTimeText.text = string.Format(
                LocalizationManager.Instance.GetTranslation("TRIAL_DURATION"),
                PremiumManager.Instance.TrialDurationDays
            );
        }
        
        // Update prices from IAPManager
        for (int i = 0; i < priceTexts.Length; i++)
        {
            priceTexts[i].text = IAPManager.Instance.GetProductPrice(i);
        }
    }
    
    private void AnimateElements()
    {
        // Animate premium icon
        premiumIcon.localScale = Vector3.zero;
        premiumIcon.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        
        // Animate buttons
        for (int i = 0; i < purchaseButtons.Length; i++)
        {
            purchaseButtons[i].transform.localScale = Vector3.zero;
            purchaseButtons[i].transform.DOScale(1, 0.3f)
                .SetDelay(0.1f * i)
                .SetEase(Ease.OutBack);
        }
    }
    
    private void StartTrial()
    {
        PremiumManager.Instance.StartTrial();
        AudioManager.Instance.PlaySound("success");
        Hide();
    }
    
    private async void OnPurchaseSelected(int index)
    {
        // Disable buttons during purchase
        SetButtonsInteractable(false);
        
        bool success = await IAPManager.Instance.PurchasePremium(index);
        if (success)
        {
            AudioManager.Instance.PlaySound("purchase_success");
            Hide();
        }
        else
        {
            AudioManager.Instance.PlaySound("purchase_failed");
            UIManager.Instance.ShowMessage(
                LocalizationManager.Instance.GetTranslation("PURCHASE_FAILED")
            );
        }
        
        SetButtonsInteractable(true);
    }
    
    private void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in purchaseButtons)
        {
            button.interactable = interactable;
        }
        startTrialButton.interactable = interactable;
    }
} 