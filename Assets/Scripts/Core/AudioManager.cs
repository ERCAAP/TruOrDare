using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [System.Serializable]
    public class SoundEffect
    {
        public AudioClip clip;
        public string id;
        public float volume = 1f;
        public bool loop = false;
    }
    
    [SerializeField] private SoundEffect[] soundEffects;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    private Dictionary<string, SoundEffect> soundDictionary;
    
    private void Awake()
    {
        Instance = this;
        InitializeSounds();
    }
    
    private void InitializeSounds()
    {
        soundDictionary = new Dictionary<string, SoundEffect>();
        foreach (var sound in soundEffects)
        {
            soundDictionary[sound.id] = sound;
        }
    }
    
    public void PlaySound(string soundId)
    {
        if (soundDictionary.TryGetValue(soundId, out SoundEffect sound))
        {
            sfxSource.PlayOneShot(sound.clip, sound.volume);
        }
    }
    
    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }
    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void Initialize()
    {
        // TODO: Initialize audio settings
    }
} 