using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturntoMenu : MonoBehaviour
{
    public TransitionManager manager;

    public OtherAudioManager otherAudioManager;

    public void ReturnToMenu ()
    {
        Debug.Log("Returning To Main Menu");
        manager.loadMainMenu();
        otherAudioManager.PlaySFX(otherAudioManager.clickButton);
        otherAudioManager.StopMusic();
    }
}
