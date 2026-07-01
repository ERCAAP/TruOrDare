using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }
    public string CurrentPlayerId { get; private set; }

    [SerializeField] private Sprite[] defaultAvatars;

    private void Awake()
    {
        Instance = this;
    }

    public PlayerData GetPlayerData(string playerId)
    {
        string defaultName = $"Player_{Random.Range(1000, 9999)}";
        Sprite defaultAvatar = defaultAvatars[Random.Range(0, defaultAvatars.Length)];
        return new PlayerData(defaultName, defaultAvatar);
    }

    public static void SavePlayerData(PlayerData playerData)
    {
        // TODO: Implement player data saving
    }

    public void SetCurrentPlayer(string playerId)
    {
        CurrentPlayerId = playerId;
    }

    public PlayerData CreateNewPlayer()
    {
        string defaultName = $"Player_{Random.Range(1000, 9999)}";
        Sprite defaultAvatar = defaultAvatars[Random.Range(0, defaultAvatars.Length)];
        return new PlayerData(defaultName, defaultAvatar);
    }
} 