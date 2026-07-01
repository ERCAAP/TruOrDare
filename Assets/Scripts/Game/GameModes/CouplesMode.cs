using UnityEngine;

public class CouplesMode : GameModeBase
{
    private const int POINTS_PER_COMPLETION = 150;
    private const int MAX_ROUNDS = 10;
    private int currentRound = 0;
    
    public override void StartRound()
    {
        if (currentRound >= MAX_ROUNDS)
        {
            GameManager.Instance.ChangeGameState(GameState.GameOver);
            return;
        }
        
        PlayerData currentPlayer = GetCurrentPlayer();
        QuestionType questionType = Random.value > 0.5f ? QuestionType.Truth : QuestionType.Dare;
        currentQuestion = QuestionManager.Instance.GetQuestion(questionType);
        
        GameEvents.OnNewRoundStarted?.Invoke(currentPlayer, currentQuestion);
        currentRound++;
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
        return currentRound >= MAX_ROUNDS;
    }
} 