using UnityEngine;
using System.Collections.Generic;

public class FriendsMode : GameModeBase
{
    private const int POINTS_PER_COMPLETION = 100;
    
    public override void StartRound()
    {
        PlayerData currentPlayer = GetCurrentPlayer();
        QuestionType questionType = Random.value > 0.5f ? QuestionType.Truth : QuestionType.Dare;
        currentQuestion = QuestionManager.Instance.GetQuestion(questionType);
        
        GameEvents.OnNewRoundStarted?.Invoke(currentPlayer, currentQuestion);
    }
    
    public override void ProcessAnswer(bool isCompleted)
    {
        if (isCompleted)
        {
            GetCurrentPlayer().AddPoints(POINTS_PER_COMPLETION);
        }
        NextPlayer();
    }
    
    public override bool CheckGameEnd()
    {
        return players.Exists(p => p.Points >= GameManager.Instance.gameSettings.pointsToWin);
    }
} 