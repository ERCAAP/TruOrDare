using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SettingsPanel : BasePanel
{
    [Header("Audio Settings")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle vibrationToggle;
    
    [Header("Language Settings")]
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private Image languageFlag;
    [SerializeField] private Sprite[] languageFlags;
    
    [Header("Other Settings")]
    [SerializeField] private Button restoreButton;
    [SerializeField] private Button contactButton;
    [SerializeField] private Button rateUsButton;
    [SerializeField] private Button backButton;
    
    protected override void Awake()
    {
        base.Awake();
        InitializeUI();
        InitializeListeners();
    }
    
    private void InitializeUI()
    {
        // Set initial values
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        vibrationToggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;
        
        // Setup language dropdown
        SetupLanguageDropdown();
    }
    
    private void InitializeListeners()
    {
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        vibrationToggle.onValueChanged.AddListener(OnVibrationChanged);
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        
        restoreButton.onClick.AddListener(OnRestorePurchasesClicked);
        contactButton.onClick.AddListener(OpenContactSupport);
        rateUsButton.onClick.AddListener(OpenRateUs);
        backButton.onClick.AddListener(Hide);
    }
    
    private void SetupLanguageDropdown()
    {
        languageDropdown.ClearOptions();
        
        var languages = System.Enum.GetNames(typeof(Language));
        languageDropdown.AddOptions(new System.Collections.Generic.List<string>(languages));
        
        int currentLanguage = (int)LocalizationManager.Instance.CurrentLanguage;
        languageDropdown.value = currentLanguage;
        UpdateLanguageFlag(currentLanguage);
    }
    
    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
        AudioManager.Instance.PlaySound("button_click");
    }
    
    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        AudioManager.Instance.PlaySound("button_click");
    }
    
    private void OnVibrationChanged(bool enabled)
    {
        PlayerPrefs.SetInt("Vibration", enabled ? 1 : 0);
        if (enabled)
        {
            Handheld.Vibrate();
        }
    }
    
    private void OnLanguageChanged(int index)
    {
        LocalizationManager.Instance.SetLanguage((Language)index);
        UpdateLanguageFlag(index);
        AudioManager.Instance.PlaySound("button_click");
    }
    
    private void UpdateLanguageFlag(int index)
    {
        if (index < languageFlags.Length)
        {
            languageFlag.sprite = languageFlags[index];
        }
    }
    
    private async void OnRestorePurchasesClicked()
    {
        await IAPManager.Instance.RestorePurchases();
        // ... geri kalan kod
    }
    
    private void OpenContactSupport()
    {
        Application.OpenURL("mailto:support@yourgame.com");
    }
    
    private void OpenRateUs()
    {
        #if UNITY_ANDROID
            Application.OpenURL("market://details?id=" + Application.identifier);
        #elif UNITY_IOS
            Application.OpenURL("itms-apps://itunes.apple.com/app/id" + Application.identifier);
        #endif
    }
    
    public override void Show()
    {
        base.Show();
        
        // Animate sliders
        musicSlider.transform.localScale = Vector3.zero;
        sfxSlider.transform.localScale = Vector3.zero;
        
        musicSlider.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetDelay(0.1f);
        sfxSlider.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetDelay(0.2f);
    }
} 