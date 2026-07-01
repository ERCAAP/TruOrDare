using UnityEngine;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }
    
    public Language CurrentLanguage { get; private set; }
    
    [SerializeField] private TextAsset[] languageFiles;
    private Dictionary<string, Dictionary<Language, string>> translations;
    
    private void Awake()
    {
        Instance = this;
        LoadTranslations();
    }
    
    public void SetLanguage(Language language)
    {
        CurrentLanguage = language;
        GameEvents.OnLanguageChanged?.Invoke(language);
        PlayerPrefs.SetInt("Language", (int)language);
    }
    
    public string GetTranslation(string key)
    {
        if (translations.TryGetValue(key, out var languageDict))
        {
            if (languageDict.TryGetValue(CurrentLanguage, out string translation))
            {
                return translation;
            }
        }
        return key;
    }

    private void LoadTranslations()
    {
        // TODO: Load language files
    }

    public void Initialize()
    {
        LoadTranslations();
    }
}

public enum Language
{
    Turkish,
    English,
    French,
    German,
    Spanish,
    Italian,
    Portuguese,
    Russian
} 