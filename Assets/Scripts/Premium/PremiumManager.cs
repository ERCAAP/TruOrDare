using UnityEngine;
using System;

public class PremiumManager : MonoBehaviour
{
    public static PremiumManager Instance { get; private set; }
    
    public bool IsPremium { get; private set; }
    public DateTime TrialEndDate { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        LoadPremiumStatus();
    }
    
    public bool CanAccessGameMode(GameModeType mode)
    {
        if (IsPremium) return true;
        return mode == GameModeType.Friends; // Sadece arkadaş modu ücretsiz
    }
    
    public void StartTrial()
    {
        TrialEndDate = DateTime.Now.AddDays(3);
        SavePremiumStatus();
    }
    
    public void ActivatePremium()
    {
        IsPremium = true;
        SavePremiumStatus();
        GameEvents.OnPremiumStatusChanged?.Invoke(true);
    }
    
    private void LoadPremiumStatus()
    {
        IsPremium = PlayerPrefs.GetInt("Premium", 0) == 1;
        string savedDate = PlayerPrefs.GetString("TrialEnd", "");
        if (DateTime.TryParse(savedDate, out DateTime date))
        {
            TrialEndDate = date;
        }
    }
    
    private void SavePremiumStatus()
    {
        PlayerPrefs.SetInt("Premium", IsPremium ? 1 : 0);
        PlayerPrefs.SetString("TrialEnd", TrialEndDate.ToString());
        PlayerPrefs.Save();
    }

    public bool CanAccessFeature(PremiumFeature feature)
    {
        // TODO: Check if feature is available
        return true;
    }

    public bool CanStartTrial => !HasActiveTrial && !IsPremium;

    public int TrialDurationDays => 7;

    public bool HasActiveTrial => !IsPremium && DateTime.Now < TrialEndDate;
} 