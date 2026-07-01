using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    [Header("Language")]
    [SerializeField] private TMP_Dropdown languageDropdown;
    
    [Header("Other")]
    [SerializeField] private Toggle vibrationToggle;
    
    private void Awake()
    {
        SetupUI();
    }
    
    private void SetupUI()
    {
        // Load saved settings
        var settings = SettingsData.LoadFromPrefs();
        
        musicSlider.value = settings.MusicVolume;
        sfxSlider.value = settings.SFXVolume;
        vibrationToggle.isOn = settings.VibrationEnabled;
        
        // Setup listeners
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        vibrationToggle.onValueChanged.AddListener(OnVibrationToggled);
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }
    
    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }
    
    private void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
    
    private void OnVibrationToggled(bool enabled)
    {
        // Save vibration setting
        PlayerPrefs.SetInt("Vibration", enabled ? 1 : 0);
    }
    
    private void OnLanguageChanged(int index)
    {
        LocalizationManager.Instance.SetLanguage((Language)index);
    }
} 