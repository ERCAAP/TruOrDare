using UnityEngine;

namespace Game.Players
{
    public class Player : MonoBehaviour
    {
        public PlayerData PlayerData { get; private set; }

        public void Initialize(PlayerData playerData)
        {
            PlayerData = playerData;
        }
    }
} 