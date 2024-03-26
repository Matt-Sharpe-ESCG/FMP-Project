using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainOptionsMenu : MonoBehaviour
{
    public OtherAudioManager newAudioManager;

    
    public void volumeSettingsMenu()
    {
        newAudioManager.PlaySFX(newAudioManager.clickButton);        
    }

    public void graphicsSettingsMenu()
    {       
        newAudioManager.PlaySFX(newAudioManager.clickButton);
    }

    public void backButton()
    {  
        newAudioManager.PlaySFX(newAudioManager.clickButton);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        newAudioManager.PlaySFX(newAudioManager.clickButton);
    }

    public void instructionsMenu()
    {
        newAudioManager.PlaySFX(newAudioManager.clickButton);
    }
}
