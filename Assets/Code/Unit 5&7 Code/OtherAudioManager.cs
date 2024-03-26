using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherAudioManager : MonoBehaviour
{
    public static OtherAudioManager Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(Instance);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

        [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip menuMusic;
    public AudioClip transition;
    public AudioClip clickButton;
    public AudioClip gameMusic;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicSource.clip = menuMusic;
            musicSource.Play();
        }
        else
        {
            musicSource.clip = gameMusic;
            musicSource.Play();
        }              
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        SFXSource.mute = !SFXSource.mute;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void playGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }
}
