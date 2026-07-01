using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "TruthOrDare/AudioSettings")]
public class AudioSettings : ScriptableObject
{
    [Header("UI Sounds")]
    public AudioClip buttonClick;
    public AudioClip playerAdded;
    public AudioClip playerRemoved;
    public AudioClip roundStart;
    public AudioClip roundEnd;
    public AudioClip victory;

    [Header("Game Sounds")]
    public AudioClip questionReveal;
    public AudioClip timeWarning;
    public AudioClip timeUp;
} 