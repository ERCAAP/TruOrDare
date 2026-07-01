using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    [Serializable]
    public class PlayerData
    {
        public string PlayerName { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public List<Achievement> Achievements { get; set; }
        public Dictionary<GameModeType, int> HighScores { get; set; }

        public PlayerData()
        {
            Achievements = new List<Achievement>();
            HighScores = new Dictionary<GameModeType, int>();
        }
    }
} 