using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicType
{
    open, win, lose
}

public class AudioController : MonoBehaviour
{
    [Header("Music")]
    public AudioSource audioSource;
    public AudioClip openMusic, winMusic, loseMusic;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip sfxClip;

    [Header("Reserve Source")]
    public AudioSource[] reserveSources;

    public static AudioController Instance;
    private string musicKey = "BackgroundMusic";
    private string sfxKey = "SFX";
    public float musicVolume
    {
        set
        {
            PlayerPrefs.SetFloat(musicKey, value);
            audioSource.volume = value;
        }
        get
        {
            return PlayerPrefs.GetFloat(musicKey, 1);
        }
    }

    public float sfxVolume
    {
        set
        {
            PlayerPrefs.SetFloat(sfxKey, value);
        }
        get
        {
            return PlayerPrefs.GetFloat(sfxKey, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        PlayMusic(MusicType.open);
    }

    /// <summary>
    /// Play sound effect
    /// </summary>
    /// <param name="source">The audio source to play sound effect</param>
    /// <param name="audioClip">The audioclip to be played</param>
    public void PlaySFX(AudioSource source, AudioClip audioClip)
    {
        if (audioSource.isPlaying) return;

        source.PlayOneShot(audioClip, sfxVolume);
    }

    /// <summary>
    /// Play music
    /// </summary>
    /// <param name="type">The type of music to be played</param>
    public void PlayMusic(MusicType type)
    {
        if (audioSource.isPlaying) return;

        switch (type)
        {
            case MusicType.open:
                audioSource.PlayOneShot(openMusic, musicVolume);
                break;
            case MusicType.win:
                audioSource.PlayOneShot(winMusic, musicVolume);
                break;
            case MusicType.lose:
                audioSource.PlayOneShot(loseMusic, musicVolume);
                break;
        }
    }

    /// <summary>
    /// Get the length of the audio clip
    /// </summary>
    /// <param name="type">The type of the music</param>
    /// <returns></returns>
    public float GetMusicLength(MusicType type)
    {
        float length = 0;
        switch (type)
        {
            case MusicType.open:
                length = openMusic.length;
                break;
            case MusicType.win:
                length = openMusic.length;
                break;
            case MusicType.lose:
                length = openMusic.length;
                break;
        }
        return length;
    }

    /// <summary>
    /// Play sound effect when player interact with to UI
    /// </summary>
    public void PlaySelectEventSFX()
    {
        if (audioSource.isPlaying) return;

        sfxSource.PlayOneShot(sfxClip, sfxVolume);
    }

    /// <summary>
    /// Play sound or music using available audio source
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayUsingReserveSource(AudioClip audioClip)
    {
        if (audioSource.isPlaying) return;

        foreach (AudioSource source in reserveSources)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(audioClip, sfxVolume);
                break;
            }
        }
    }
}
