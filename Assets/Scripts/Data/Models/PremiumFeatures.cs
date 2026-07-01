using UnityEngine;

[System.Serializable]
public class PremiumFeatures
{
    public bool UnlimitedQuestions;
    public bool AllGameModes;
    public bool CustomAvatars;
    public bool AdFree;
    public bool ExclusiveContent;
    
    public static PremiumFeatures Default => new PremiumFeatures
    {
        UnlimitedQuestions = false,
        AllGameModes = false,
        CustomAvatars = false,
        AdFree = false,
        ExclusiveContent = false
    };
    
    public static PremiumFeatures Premium => new PremiumFeatures
    {
        UnlimitedQuestions = true,
        AllGameModes = true,
        CustomAvatars = true,
        AdFree = true,
        ExclusiveContent = true
    };
} 