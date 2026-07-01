using System;

public static class GameEvents
{
    public static Action<PlayerData> OnPlayerAdded;
    public static Action<PlayerData> OnPlayerRemoved;
    public static Action<PlayerData> OnPlayerScoreChanged;
    public static Action<PlayerData, QuestionData> OnNewRoundStarted;
    public static Action<bool> OnPremiumStatusChanged;
    public static Action<GameState> OnGameStateChanged;
    public static Action<Language> OnLanguageChanged;
} 