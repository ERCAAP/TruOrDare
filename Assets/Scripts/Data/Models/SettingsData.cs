using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float MusicVolume = 1f;
    public float SFXVolume = 1f;
    public bool VibrationEnabled = true;
    public Language CurrentLanguage = Language.English;
    
    public void SaveToPrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        PlayerPrefs.SetInt("Vibration", VibrationEnabled ? 1 : 0);
        PlayerPrefs.SetInt("Language", (int)CurrentLanguage);
        PlayerPrefs.Save();
    }
    
    public static SettingsData LoadFromPrefs()
    {
        return new SettingsData
        {
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f),
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f),
            VibrationEnabled = PlayerPrefs.GetInt("Vibration", 1) == 1,
            CurrentLanguage = (Language)PlayerPrefs.GetInt("Language", 0)
        };
    }
} 