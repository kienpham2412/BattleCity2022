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

    /// <summary>
    /// Change music volume
    /// </summary>
    public void ChangeMusicVolume()
    {
        AudioController.Instance.musicVolume = backgroundMusicSlider.value;
    }

    /// <summary>
    /// Change sound effect volume
    /// </summary>
    public void ChangeSFXVolume()
    {
        AudioController.Instance.sfxVolume = sfxSlider.value;
    }
}
