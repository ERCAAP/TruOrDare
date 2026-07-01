using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "TruthOrDare/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Player Settings")]
    public int minPlayers = 2;
    public int maxPlayers = 8;
    public float playerTurnTime = 30f;

    [Header("Game Rules")]
    public int pointsToWin = 1000;
    public int pointsPerQuestion = 100;
    public int bonusPoints = 50;

    [Header("Premium Settings")]
    public int freeQuestionsPerDay = 20;
    public bool unlimitedQuestionsInPremium = true;
} 