using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerSelectionManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button addPlayerButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Transform playerCardsContainer;
    [SerializeField] private GameObject playerCardPrefab;
    [SerializeField] private Transform headerAnimation;
    
    [Header("Settings")]
    [SerializeField] private float cardAnimationDuration = 0.3f;
    [SerializeField] private Vector3 cardSpawnOffset = new Vector3(0, 100f, 0);
    
    private List<PlayerCardUI> playerCards = new List<PlayerCardUI>();
    private List<PlayerData> players = new List<PlayerData>();

    private void Awake()
    {
        SetupButtons();
        UpdateContinueButton();
    }

    private void SetupButtons()
    {
        addPlayerButton.onClick.AddListener(AddNewPlayer);
        continueButton.onClick.AddListener(OnContinueClicked);
    }

    public void AddNewPlayer()
    {
        if (players.Count >= GameManager.Instance.MaxPlayers)
        {
            UIManager.Instance.ShowMessage("Maximum player limit reached!");
            return;
        }

        // Create player data
        PlayerData newPlayer = new PlayerData($"Player {players.Count + 1}", Gender.Male);
        players.Add(newPlayer);

        // Instantiate and setup player card
        GameObject cardObj = Instantiate(playerCardPrefab, playerCardsContainer);
        PlayerCardUI cardUI = cardObj.GetComponent<PlayerCardUI>();
        
        // Initial spawn position with offset
        cardObj.transform.localPosition += cardSpawnOffset;
        
        // Setup card
        cardUI.Initialize(newPlayer, OnPlayerDataChanged, OnPlayerRemoved);
        playerCards.Add(cardUI);

        // Animate card entry
        cardObj.transform.DOLocalMove(Vector3.zero, cardAnimationDuration)
            .SetEase(Ease.OutBack);

        // Animate header
        headerAnimation.DOPunchScale(Vector3.one * 1.1f, 0.3f);
        
        UpdateContinueButton();
    }

    private void OnPlayerDataChanged(PlayerData player)
    {
        // Handle any updates needed when player data changes
        UpdateContinueButton();
    }

    private void OnPlayerRemoved(PlayerData player)
    {
        int index = players.IndexOf(player);
        if (index != -1)
        {
            players.RemoveAt(index);
            PlayerCardUI cardUI = playerCards[index];
            playerCards.RemoveAt(index);
            
            // Animate card removal
            cardUI.transform.DOScale(Vector3.zero, cardAnimationDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(cardUI.gameObject));
                
            UpdateContinueButton();
        }
    }

    private void UpdateContinueButton()
    {
        continueButton.interactable = players.Count >= GameManager.Instance.MinPlayers;
    }

    private void OnContinueClicked()
    {
        // Save players to game manager or relevant system
        GameManager.Instance.StartGame(GameModeType.Friends);
    }
} 