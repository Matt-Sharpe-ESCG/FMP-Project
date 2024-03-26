using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public OtherAudioManager otherAudioManager;

    public void playTransition()
    {
        otherAudioManager.PlaySFX(otherAudioManager.transition);
    }
}
