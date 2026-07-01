using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AchievementItemUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Image lockIcon;
    [SerializeField] private ParticleSystem unlockParticles;
    
    [Header("Visual Settings")]
    [SerializeField] private Color lockedColor = Color.gray;
    [SerializeField] private Color unlockedColor = Color.white;
    
    public string AchievementId { get; private set; }
    private bool isUnlocked;
    
    public void Initialize(Achievement achievement)
    {
        AchievementId = achievement.Id;
        
        titleText.text = LocalizationManager.Instance.GetTranslation(achievement.TitleKey);
        descriptionText.text = LocalizationManager.Instance.GetTranslation(achievement.DescriptionKey);
        iconImage.sprite = achievement.Icon;
        
        UpdateProgress(achievement.Progress);
        UpdateUnlockState(achievement.IsUnlocked);
    }
    
    public void UpdateProgress(float progress)
    {
        progressBar.value = progress;
        progressText.text = $"{(progress * 100):F0}%";
        
        if (progress >= 1 && !isUnlocked)
        {
            UpdateUnlockState(true);
        }
    }
    
    public void UpdateUnlocked()
    {
        if (!isUnlocked)
        {
            UpdateUnlockState(true);
            PlayUnlockAnimation();
        }
    }
    
    private void UpdateUnlockState(bool unlocked)
    {
        isUnlocked = unlocked;
        lockIcon.gameObject.SetActive(!unlocked);
        
        Color targetColor = unlocked ? unlockedColor : lockedColor;
        iconImage.color = targetColor;
        titleText.color = targetColor;
    }
    
    private void PlayUnlockAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(transform.DOScale(1.1f, 0.2f))
                .Append(transform.DOScale(1f, 0.2f))
                .SetEase(Ease.OutBack);
        
        if (unlockParticles != null)
        {
            unlockParticles.Play();
        }
    }
} 