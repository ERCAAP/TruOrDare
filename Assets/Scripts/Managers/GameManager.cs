using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameSettings gameSettings;
    public List<PlayerData> Players { get; private set; } = new List<PlayerData>();
    
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeGame();
    }
    
    private void InitializeGame()
    {
        // Initialize all managers
        AudioManager.Instance.Initialize();
        LocalizationManager.Instance.Initialize();
        UIManager.Instance.Initialize();
    }
    
    public void AddPlayer(PlayerData player)
    {
        Players.Add(player);
        GameEvents.OnPlayerAdded?.Invoke(player);
    }
    
    public void RemovePlayer(PlayerData player)
    {
        Players.Remove(player);
        GameEvents.OnPlayerRemoved?.Invoke(player);
    }
    
    public void StartGame(GameModeType mode)
    {
        if (Players.Count < gameSettings.minPlayers)
        {
            UIManager.Instance.ShowMessage("Not enough players!");
            return;
        }
        
        GameStateManager.Instance.ChangeState(GameState.Playing);
        GameFlowManager.Instance.StartGame(mode, Players);
    }
    
    public void RestartGame()
    {
        foreach (var player in Players)
        {
            player.ResetPoints();
        }
        StartGame(GameModeType.Friends);
    }
    
    public void ReturnToMainMenu()
    {
        Players.Clear();
        GameStateManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.ShowPanel(PanelType.MainMenu);
    }
} 