using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameSettings gameSettings;
    
    public GameState CurrentState { get; private set; }
    public GameModeBase CurrentGameMode { get; private set; }
    
    public event Action<GameState> OnGameStateChanged;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializeGame()
    {
        AudioManager.Instance.Initialize();
        LocalizationManager.Instance.Initialize();
        UIManager.Instance.Initialize();
    }
    
    public void StartGame(GameModeType modeType)
    {
        CurrentGameMode = modeType switch
        {
            GameModeType.Friends => new FriendsMode(),
            GameModeType.Couples => new CouplesMode(),
            _ => throw new ArgumentException("Invalid game mode")
        };
        
        ChangeGameState(GameState.Playing);
    }
    
    public void ChangeGameState(GameState newState)
    {
        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }

    public void RestartGame()
    {
        // TODO: Implement game restart
    }

    public void ReturnToMainMenu()
    {
        // TODO: Return to main menu
    }

    public int MinPlayers => 2; // Minimum player count
    public int MaxPlayers => gameSettings.MaxPlayers;
}

public enum GameState
{
    MainMenu,
    Loading,
    Playing,
    Paused,
    GameOver
} 