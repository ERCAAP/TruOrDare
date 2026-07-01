using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelectionUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCardsContainer;
    [SerializeField] private Button addPlayerButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private GameObject playerCardPrefab;
    
    [Header("Animation")]
    [SerializeField] private float cardSpacing = 20f;
    [SerializeField] private float cardAnimationDuration = 0.3f;
    
    private void Awake()
    {
        SetupButtons();
        UpdateUI();
    }
    
    private void OnEnable()
    {
        GameEvents.OnPlayerAdded += OnPlayerAdded;
        GameEvents.OnPlayerRemoved += OnPlayerRemoved;
    }
    
    private void OnDisable()
    {
        GameEvents.OnPlayerAdded -= OnPlayerAdded;
        GameEvents.OnPlayerRemoved -= OnPlayerRemoved;
    }
    
    private void SetupButtons()
    {
        addPlayerButton.onClick.AddListener(OnAddPlayerClicked);
        continueButton.onClick.AddListener(OnContinueClicked);
    }
    
    private void OnAddPlayerClicked()
    {
        if (GameManager.Instance.Players.Count >= GameManager.Instance.gameSettings.maxPlayers)
        {
            UIManager.Instance.ShowMessage("Maximum player limit reached!");
            return;
        }
        
        var newPlayer = new PlayerData($"Player {GameManager.Instance.Players.Count + 1}", Gender.Male);
        GameManager.Instance.AddPlayer(newPlayer);
    }
    
    private void OnContinueClicked()
    {
        GameManager.Instance.StartGame(GameModeType.Friends);
    }
    
    private void OnPlayerAdded(PlayerData player)
    {
        var cardObj = Instantiate(playerCardPrefab, playerCardsContainer);
        var cardUI = cardObj.GetComponent<PlayerCardUI>();
        cardUI.Initialize(player);
        
        // Animate card entry
        cardObj.transform.localScale = Vector3.zero;
        cardObj.transform.DOScale(Vector3.one, cardAnimationDuration).SetEase(Ease.OutBack);
        
        UpdateUI();
    }
    
    private void OnPlayerRemoved(PlayerData player)
    {
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        int playerCount = GameManager.Instance.Players.Count;
        continueButton.interactable = playerCount >= GameManager.Instance.gameSettings.minPlayers;
        addPlayerButton.interactable = playerCount < GameManager.Instance.gameSettings.maxPlayers;
        
        headerText.text = $"Players ({playerCount}/{GameManager.Instance.gameSettings.maxPlayers})";
    }
} 