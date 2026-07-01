using System.Collections.Generic;
using UnityEngine;

public abstract class GameModeBase
{
    protected List<PlayerData> players = new List<PlayerData>();
    protected int currentPlayerIndex = 0;
    protected QuestionData currentQuestion;

    public virtual void Initialize(List<PlayerData> gamePlayers)
    {
        players = gamePlayers;
        currentPlayerIndex = 0;
    }

    public abstract void StartRound();
    public abstract void ProcessAnswer(bool isCompleted);
    public abstract bool CheckGameEnd();

    protected PlayerData GetCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

    protected void NextPlayer()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    }
}

public enum GameModeType
{
    Friends,
    Couples
} 