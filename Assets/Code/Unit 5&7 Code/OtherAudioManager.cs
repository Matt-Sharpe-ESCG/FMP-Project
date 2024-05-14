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
    public AudioClip spaceAmbience;
    public AudioClip desertAmbience;
    public AudioClip earthAmbience1;
    public AudioClip earthAmbience2;
    public AudioClip lunarAmbience;
    public AudioClip gunShot;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicSource.clip = menuMusic;
            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            musicSource.clip = lunarAmbience;
            musicSource.Play();
            musicSource.loop = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            musicSource.clip = desertAmbience;
            musicSource.Play();
            musicSource.loop = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            musicSource.clip = spaceAmbience;
            musicSource.Play();
            musicSource.loop = true;
        }
        else
        {
            Debug.Log("No Appropriate Scene For Track");
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

    public void endLoop()
    {
        musicSource.loop = false;
    }
}
