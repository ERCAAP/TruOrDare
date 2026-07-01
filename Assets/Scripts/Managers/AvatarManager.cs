using UnityEngine;
using System.Collections.Generic;

public class AvatarManager : MonoBehaviour
{
    public static AvatarManager Instance { get; private set; }

    [SerializeField] private List<AvatarData> maleAvatars;
    [SerializeField] private List<AvatarData> femaleAvatars;
    [SerializeField] private Sprite defaultMaleAvatar;
    [SerializeField] private Sprite defaultFemaleAvatar;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetAvatar(Gender gender, int index)
    {
        var avatarList = gender == Gender.Male ? maleAvatars : femaleAvatars;
        
        if (index < 0 || index >= avatarList.Count)
        {
            return gender == Gender.Male ? defaultMaleAvatar : defaultFemaleAvatar;
        }

        return avatarList[index].avatarSprite;
    }

    public List<AvatarData> GetAvatarsForGender(Gender gender)
    {
        return gender == Gender.Male ? maleAvatars : femaleAvatars;
    }
} 