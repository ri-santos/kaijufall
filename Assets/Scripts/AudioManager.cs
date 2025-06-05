using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 0.7f;
    public bool loop = false;
    [HideInInspector] public AudioSource source;
}

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    
    [Header("Music")]
    public Sound menuMusic;
    public Sound phase1Music;
    public Sound phase2Music;
    
    [Header("SFX")]
    public Sound phaseTransition;
    public Sound buttonClick;
    
    private Sound currentMusic;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSounds();
        } else {
            Destroy(gameObject);
        }
    }

    void InitializeSounds() {
        // Setup music
        CreateAudioSource(menuMusic);
        CreateAudioSource(phase1Music);
        CreateAudioSource(phase2Music);
        
        // Setup SFX
        CreateAudioSource(phaseTransition);
        CreateAudioSource(buttonClick);
    }

    void CreateAudioSource(Sound sound) {
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.loop = sound.loop;
    }

    public void PlayMusic(Sound music) {
        if (currentMusic != null) {
            currentMusic.source.Stop();
        }
        
        currentMusic = music;
        music.source.Play();
    }

    public void PlaySFX(Sound sfx) {
        sfx.source.PlayOneShot(sfx.clip, sfx.volume);
    }
}