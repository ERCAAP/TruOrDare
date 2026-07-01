using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerCardUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button maleButton;
    [SerializeField] private Button femaleButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Image avatarImage;
    [SerializeField] private Button changeAvatarButton;
    
    [Header("Visual Feedback")]
    [SerializeField] private Color selectedGenderColor = Color.green;
    [SerializeField] private Color unselectedGenderColor = Color.white;
    
    private PlayerData playerData;
    private Action<PlayerData> onDataChanged;
    private Action<PlayerData> onRemoved;

    public void Initialize(PlayerData data, Action<PlayerData> dataChangedCallback, Action<PlayerData> removedCallback)
    {
        playerData = data;
        onDataChanged = dataChangedCallback;
        onRemoved = removedCallback;

        SetupUI();
        UpdateVisuals();
    }

    private void SetupUI()
    {
        nameInput.text = playerData.Name;
        nameInput.onValueChanged.AddListener(OnNameChanged);

        maleButton.onClick.AddListener(() => OnGenderSelected(Gender.Male));
        femaleButton.onClick.AddListener(() => OnGenderSelected(Gender.Female));
        removeButton.onClick.AddListener(OnRemoveClicked);
        changeAvatarButton.onClick.AddListener(OnChangeAvatarClicked);
    }

    private void OnNameChanged(string newName)
    {
        playerData.Name = newName;
        onDataChanged?.Invoke(playerData);
    }

    private void OnGenderSelected(Gender gender)
    {
        playerData.Gender = gender;
        UpdateVisuals();
        onDataChanged?.Invoke(playerData);
    }

    private void UpdateVisuals()
    {
        maleButton.GetComponent<Image>().color = 
            playerData.Gender == Gender.Male ? selectedGenderColor : unselectedGenderColor;
        femaleButton.GetComponent<Image>().color = 
            playerData.Gender == Gender.Female ? selectedGenderColor : unselectedGenderColor;
            
        // Update avatar based on gender and avatar index
        // avatarImage.sprite = AvatarManager.Instance.GetAvatar(playerData.Gender, playerData.AvatarIndex);
    }

    private void OnRemoveClicked()
    {
        onRemoved?.Invoke(playerData);
    }

    private void OnChangeAvatarClicked()
    {
        // Open avatar selection panel
        UIManager.Instance.ShowAvatarSelectionPanel();
    }
} 