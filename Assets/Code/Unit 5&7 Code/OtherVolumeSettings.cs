using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OtherVolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] public Slider _musicSlider, _sfxSlider;
    public OtherAudioManager newAudioManager;
    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
            
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }
    public void ToggleMusic()
    {
        newAudioManager.ToggleMusic();
    }

    public void ToggleSFX()
    {
        newAudioManager.ToggleSFX();
    }
    public void SetMusicVolume()
    {
        float volumeMusic = _musicSlider.value;
        mainMixer.SetFloat("music", Mathf.Log10(volumeMusic)* 20);
        PlayerPrefs.SetFloat("musicVolume", volumeMusic);
    }

    public void SetSFXVolume()
    {
        float volumeSFX = _sfxSlider.value;
        mainMixer.SetFloat("SFX", Mathf.Log10(volumeSFX) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volumeSFX);
    }

    private void LoadMusicVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();       
    }

    private void LoadSFXVolume()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolume();
    }

    public void backButton()
    {
        newAudioManager.PlaySFX(newAudioManager.clickButton);
    }
}
