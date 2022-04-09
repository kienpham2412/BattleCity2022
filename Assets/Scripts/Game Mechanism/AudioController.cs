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

    public static AudioController Instance;
    private string musicKey = "BackgroundMusic";
    private string sfxKey = "SFX";
    public float musicVolume
    {
        set
        {
            PlayerPrefs.SetFloat(musicKey, value);
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

    public void PlaySFX(AudioSource source, AudioClip audioClip)
    {
        source.PlayOneShot(audioClip, sfxVolume);
    }

    public void PlayMusic(MusicType type)
    {
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

    public void PlaySelectEventSFX()
    {
        sfxSource.PlayOneShot(sfxClip, sfxVolume);
    }
}
