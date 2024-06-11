using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] public Slider _masterslider, _musicSlider, _sfxSlider;
    [SerializeField] private GameObject crosshair, healthbar, pauseMenuUI;
    public OtherAudioManager newAudioManager;
    public TransitionManager transitionManager;
    public GameObject gameOverUI;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadMasterVolume();
        }
        else
        {
            SetMasterVolume();
        }

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

    public void SetMasterVolume()
    {
        float volumeMaster = _masterslider.value;
        mainMixer.SetFloat("master", Mathf.Log10(volumeMaster) * 20);
        PlayerPrefs.SetFloat("masterVolume", volumeMaster);
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

    private void LoadMasterVolume()
    {
        _masterslider.value = PlayerPrefs.GetFloat("masterVolume");
        SetMasterVolume();
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
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        newAudioManager.PlaySFX(newAudioManager.clickButton);
    }
    public void quitGame()
    {
        Debug.Log("QUIT");
        newAudioManager.PlaySFX(newAudioManager.clickButton);
        transitionManager.quitGameTrigger();
    }

    public void openMenu()
    {
        Debug.Log("Open Menu");
        newAudioManager.PlaySFX(newAudioManager.clickButton);
        pauseMenuUI.SetActive(true);
        healthbar.SetActive(false);
        crosshair.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == false)
        {
            gameOverUI.SetActive(true);
        }
    }
}
