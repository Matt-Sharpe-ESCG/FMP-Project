using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Audio Files
    public OtherAudioManager newAudioManager;

    public TransitionManager transitionManager;

    public void playGame()
    {
        Debug.Log("Playing Game");
        transitionManager.playGameTrigger();
        newAudioManager.PlaySFX(newAudioManager.clickButton);
        newAudioManager.StopMusic();
    }

    public void openOptions()
    {
        newAudioManager.PlaySFX(newAudioManager.clickButton);
    }

    public void quitGame()
    {
        Debug.Log("QUIT");    
        newAudioManager.PlaySFX(newAudioManager.clickButton);
        transitionManager.quitGameTrigger();
    }  
}
