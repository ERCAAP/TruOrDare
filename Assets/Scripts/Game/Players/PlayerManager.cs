using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    
    private List<PlayerData> players = new List<PlayerData>();
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void AddPlayer(PlayerData player)
    {
        players.Add(player);
        GameEvents.OnPlayerAdded?.Invoke(player);
    }
    
    public void RemovePlayer(PlayerData player)
    {
        if (players.Remove(player))
        {
            GameEvents.OnPlayerRemoved?.Invoke(player);
        }
    }
    
    public List<PlayerData> GetPlayers()
    {
        return players;
    }
} 