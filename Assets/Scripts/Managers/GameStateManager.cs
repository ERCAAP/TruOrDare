using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    
    public GameState CurrentState { get; private set; }
    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        Instance = this;
        CurrentState = GameState.MainMenu;
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
        GameEvents.OnGameStateChanged?.Invoke(newState);

        HandleStateChange(newState);
    }

    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                Time.timeScale = 1;
                break;
                
            case GameState.Playing:
                Time.timeScale = 1;
                break;
                
            case GameState.Paused:
                Time.timeScale = 0;
                break;
                
            case GameState.GameOver:
                Time.timeScale = 1;
                ShowGameOver();
                break;
        }
    }

    private void ShowGameOver()
    {
        UIManager.Instance.ShowPanel(PanelType.GameOver);
    }
} 