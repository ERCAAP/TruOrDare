using UnityEngine;

public class Achievement
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string TitleKey { get; set; }
    public string DescriptionKey { get; set; }
    public Sprite Icon { get; set; }
    public float Progress { get; set; }
    public bool IsUnlocked { get; set; }
} 