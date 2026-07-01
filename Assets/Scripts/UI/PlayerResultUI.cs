using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResultUI : MonoBehaviour
{
    [SerializeField] private Image avatarImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winnerCrown;

    public void Initialize(PlayerData player)
    {
        avatarImage.sprite = AvatarManager.Instance.GetAvatar(player.Gender, player.AvatarIndex);
        playerNameText.text = player.Name;
        scoreText.text = player.Points.ToString();
        
        // Show crown for winner
        if (winnerCrown != null)
        {
            winnerCrown.SetActive(player.Points >= GameManager.Instance.gameSettings.pointsToWin);
        }
    }
} 