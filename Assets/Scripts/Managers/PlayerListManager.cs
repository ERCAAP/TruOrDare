using UnityEngine;
using System.Collections.Generic;

public class PlayerListManager : MonoBehaviour
{
    private List<PlayerData> players = new List<PlayerData>();

    public void AddPlayer(PlayerData player)
    {
        players.Add(player);
    }

    public void RemovePlayer(string userId)
    {
        players.RemoveAll(p => p.UserId == userId);
    }

    public List<PlayerData> GetAllPlayers()
    {
        return players;
    }

    public void ClearPlayers()
    {
        players.Clear();
    }
} 