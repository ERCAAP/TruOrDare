using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerItemUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image avatarImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private Button removeButton;
    [SerializeField] private Image readyIndicator;
    
    public PlayerData PlayerData { get; private set; }
    
    private void Awake()
    {
        removeButton.onClick.AddListener(OnRemoveClicked);
    }
    
    public void Initialize(PlayerData player)
    {
        PlayerData = player;
        avatarImage.sprite = player.Avatar;
        playerNameText.text = player.PlayerName;
        
        // Entrance animation
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
    
    public void SetReady(bool isReady)
    {
        readyIndicator.gameObject.SetActive(isReady);
        
        if (isReady)
        {
            readyIndicator.transform.localScale = Vector3.zero;
            readyIndicator.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
        }
    }
    
    private void OnRemoveClicked()
    {
        AudioManager.Instance.PlaySound("button_click");
        
        // Animate removal
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(transform.DOScale(0, 0.2f))
                .OnComplete(() => {
                    PlayerManager.Instance.RemovePlayer(PlayerData);
                    Destroy(gameObject);
                });
    }
} 