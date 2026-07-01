using UnityEngine;

public class PremiumManager : MonoBehaviour
{
    public static PremiumManager Instance { get; private set; }
    
    public bool IsPremium { get; private set; }

    private void Awake()
    {
        Instance = this;
        LoadPremiumStatus();
    }

    private void LoadPremiumStatus()
    {
        IsPremium = PlayerPrefs.GetInt("Premium", 0) == 1;
    }

    public void SetPremiumStatus(bool isPremium)
    {
        IsPremium = isPremium;
        PlayerPrefs.SetInt("Premium", isPremium ? 1 : 0);
        GameEvents.OnPremiumStatusChanged?.Invoke(isPremium);
    }
} 