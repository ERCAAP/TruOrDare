using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AvatarSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject avatarButtonPrefab;
    [SerializeField] private Transform avatarContainer;
    [SerializeField] private Button closeButton;
    
    private PlayerData currentPlayer;
    private System.Action onAvatarSelected;

    public void Initialize(PlayerData player, System.Action callback)
    {
        currentPlayer = player;
        onAvatarSelected = callback;
        
        LoadAvatars();
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void LoadAvatars()
    {
        // Clear existing avatars
        foreach (Transform child in avatarContainer)
        {
            Destroy(child.gameObject);
        }

        // Load avatars for current gender
        var avatars = AvatarManager.Instance.GetAvatarsForGender(currentPlayer.Gender);
        
        foreach (var avatarData in avatars)
        {
            var buttonObj = Instantiate(avatarButtonPrefab, avatarContainer);
            var button = buttonObj.GetComponent<Button>();
            var image = buttonObj.GetComponent<Image>();
            
            image.sprite = avatarData.avatarSprite;
            
            if (avatarData.isPremium && !PremiumManager.Instance.IsPremium)
            {
                button.interactable = false;
                // Add lock icon or premium indicator
            }
            
            button.onClick.AddListener(() => SelectAvatar(avatars.IndexOf(avatarData)));
        }
    }

    private void SelectAvatar(int index)
    {
        currentPlayer.AvatarIndex = index;
        onAvatarSelected?.Invoke();
        gameObject.SetActive(false);
    }
} 