using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSettingController : MonoBehaviour
{
    public Slider backgroundMusicSlider, sfxSlider;

    private void OnEnable()
    {
        backgroundMusicSlider.SetValueWithoutNotify(AudioController.Instance.musicVolume);
        sfxSlider.SetValueWithoutNotify(AudioController.Instance.sfxVolume);
    }

    public void ChangeMusicVolume()
    {
        AudioController.Instance.musicVolume = backgroundMusicSlider.value;
    }

    public void ChangeSFXVolume()
    {
        AudioController.Instance.sfxVolume = sfxSlider.value;
    }
}
