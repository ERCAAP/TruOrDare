using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MessagePopupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private PanelAnimationController animationController;
    
    private Action onConfirm;
    
    public void Show(string message, string buttonLabel = "OK", Action callback = null)
    {
        messageText.text = message;
        buttonText.text = buttonLabel;
        onConfirm = callback;
        
        animationController.Show();
    }
    
    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmClicked);
    }
    
    private void OnConfirmClicked()
    {
        onConfirm?.Invoke();
        animationController.Hide();
    }
} 