using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerListView : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private PlayerItemUI playerItemPrefab;
    [SerializeField] private Button addPlayerButton;
    
    private Dictionary<string, PlayerItemUI> playerItems = new Dictionary<string, PlayerItemUI>();
    
    private void Awake()
    {
        addPlayerButton.onClick.AddListener(OnAddPlayerClicked);
    }
    
    private void OnEnable()
    {
        GameEvents.OnPlayerAdded += AddPlayerItem;
        GameEvents.OnPlayerRemoved += RemovePlayerItem;
    }
    
    private void OnDisable()
    {
        GameEvents.OnPlayerAdded -= AddPlayerItem;
        GameEvents.OnPlayerRemoved -= RemovePlayerItem;
    }
    
    private void AddPlayerItem(PlayerData player)
    {
        var item = Instantiate(playerItemPrefab, container);
        item.Initialize(player);
        playerItems[player.PlayerId] = item;
    }
    
    private void RemovePlayerItem(PlayerData player)
    {
        if (playerItems.TryGetValue(player.PlayerId, out var item))
        {
            Destroy(item.gameObject);
            playerItems.Remove(player.PlayerId);
        }
    }
    
    private void OnAddPlayerClicked()
    {
        if (playerItems.Count >= GameManager.Instance.MaxPlayers)
        {
            UIManager.Instance.ShowMessage(
                LocalizationManager.Instance.GetTranslation("MAX_PLAYERS_REACHED")
            );
            return;
        }
        
        UIManager.Instance.ShowAddPlayerPrompt();
    }
    
    public int PlayerCount => playerItems.Count;

    public void AddPlayer(PlayerData player)
    {
        var item = Instantiate(playerItemPrefab, container);
        item.Initialize(player);
    }
} 