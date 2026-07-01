using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class InputFieldUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI placeholderText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI errorText;
    
    private Action<string> onConfirm;
    private string initialValue;
    
    public void Initialize(string title, string initialValue, Action<string> onConfirm)
    {
        this.initialValue = initialValue;
        this.onConfirm = onConfirm;
        
        inputField.text = initialValue;
        placeholderText.text = title;
        errorText.gameObject.SetActive(false);
        
        // Setup buttons
        confirmButton.onClick.AddListener(OnConfirmClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
        
        // Animate entrance
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
        
        // Focus input field
        inputField.ActivateInputField();
    }
    
    private void OnConfirmClicked()
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            ShowError(LocalizationManager.Instance.GetTranslation("ERROR_EMPTY_INPUT"));
            return;
        }
        
        AudioManager.Instance.PlaySound("button_click");
        onConfirm?.Invoke(inputField.text);
        Hide();
    }
    
    private void OnCancelClicked()
    {
        AudioManager.Instance.PlaySound("button_click");
        Hide();
    }
    
    private void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
        
        // Shake animation
        transform.DOShakePosition(0.3f, 10, 20, 90, false, true);
    }
    
    private void Hide()
    {
        transform.DOScale(0, 0.2f).OnComplete(() => Destroy(gameObject));
    }
} 