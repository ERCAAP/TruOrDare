using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "TruthOrDare/UISettings")]
public class UISettings : ScriptableObject
{
    [Header("Canvas Settings")]
    public Vector2 referenceResolution = new Vector2(1080, 1920);
    public float matchWidthOrHeight = 0.5f;
    
    [Header("Animation Settings")]
    public float defaultAnimationDuration = 0.3f;
    public float fastAnimationDuration = 0.2f;
    public float slowAnimationDuration = 0.5f;
    
    [Header("Colors")]
    public Color primaryColor = new Color(0.2f, 0.6f, 1f);
    public Color secondaryColor = new Color(1f, 0.5f, 0f);
    public Color backgroundColor = new Color(0.1f, 0.1f, 0.1f);
    
    [Header("UI Elements")]
    public float buttonHeight = 120f;
    public float spacing = 20f;
    public float padding = 40f;
} 