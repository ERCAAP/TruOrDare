using UnityEngine;
using System.Collections;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance { get; private set; }

    [SerializeField] private float roundStartDelay = 1f;
    [SerializeField] private float answerTime = 30f;

    private GameModeBase currentGameMode;
    private bool isRoundActive;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame(GameModeType modeType, System.Collections.Generic.List<PlayerData> players)
    {
        currentGameMode = modeType switch
        {
            GameModeType.Friends => new FriendsMode(),
            GameModeType.Couples => new CouplesMode(),
            _ => new FriendsMode()
        };

        currentGameMode.Initialize(players);
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (!currentGameMode.CheckGameEnd())
        {
            yield return new WaitForSeconds(roundStartDelay);
            StartNewRound();
            yield return new WaitForSeconds(answerTime);
            
            if (isRoundActive)
            {
                TimeUp();
            }
        }

        GameManager.Instance.ChangeGameState(GameState.GameOver);
    }

    private void StartNewRound()
    {
        isRoundActive = true;
        currentGameMode.StartRound();
    }

    public void SubmitAnswer(bool completed)
    {
        if (!isRoundActive) return;
        
        isRoundActive = false;
        currentGameMode.ProcessAnswer(completed);
    }

    private void TimeUp()
    {
        isRoundActive = false;
        currentGameMode.ProcessAnswer(false);
    }
} 