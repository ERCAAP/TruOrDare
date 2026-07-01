using Game.Players;

namespace Game.Models
{
    public class UserData
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public PlayerData PlayerData { get; set; }
    }
} 