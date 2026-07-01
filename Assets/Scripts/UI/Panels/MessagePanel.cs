using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class MessagePanel : BasePanel
{
    [Header("Message Elements")]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private RectTransform messageBox;
    
    private Action onConfirm;
    private Action onCancel;
    
    protected override void Awake()
    {
        base.Awake();
        InitializeButtons();
    }
    
    private void InitializeButtons()
    {
        confirmButton.onClick.AddListener(() => {
            onConfirm?.Invoke();
            Hide();
        });
        
        cancelButton.onClick.AddListener(() => {
            onCancel?.Invoke();
            Hide();
        });
    }
    
    public void ShowMessage(string message, Action onConfirm = null, Action onCancel = null, bool showCancel = false)
    {
        this.onConfirm = onConfirm;
        this.onCancel = onCancel;
        
        messageText.text = message;
        cancelButton.gameObject.SetActive(showCancel);
        
        Show();
    }
    
    public override void Show()
    {
        base.Show();
        
        // Animate message box
        messageBox.localScale = Vector3.zero;
        messageBox.DOScale(1, 0.3f).SetEase(Ease.OutBack);
        
        // Animate buttons
        confirmButton.transform.localScale = Vector3.zero;
        confirmButton.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetDelay(0.2f);
        
        if (cancelButton.gameObject.activeSelf)
        {
            cancelButton.transform.localScale = Vector3.zero;
            cancelButton.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetDelay(0.3f);
        }
    }
    
    public override void Hide()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(messageBox.DOScale(0, 0.2f))
                .Join(confirmButton.transform.DOScale(0, 0.2f))
                .Join(cancelButton.transform.DOScale(0, 0.2f))
                .OnComplete(() => base.Hide());
    }
} 