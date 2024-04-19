using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitCode : MonoBehaviour
{
    public TransitionManager transitionManager;
    public OtherAudioManager otherAudioManager;
    public CanvasGroup group;
    public GameObject menuObject1;
    public GameObject menuObject2;
    public GameObject menuObject3;
    public int transitionTime = 1;

    public void enterDesert()
    {
        transitionManager.playTransition();
        otherAudioManager.StopMusic();
        otherAudioManager.endLoop();
        StartCoroutine(enterDesertPlanet());
    }
    public void enterLunar()
    {
        transitionManager.playTransition();
        otherAudioManager.StopMusic();
        StartCoroutine(enterLunarPlanet());
    }
    public void enterSpace()
    {
        transitionManager.playTransition();
        otherAudioManager.StopMusic();
        StartCoroutine(enterSpaceZone());
    }

    public void enterEarth()
    {
        transitionManager.playTransition();
        otherAudioManager.StopMusic();
        StartCoroutine(enterEarthPlanet());
    }

    IEnumerator enterDesertPlanet()
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(3);
        
    }
    IEnumerator enterEarthPlanet()
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(1);

    }
    IEnumerator enterLunarPlanet()
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(2);

    }

    IEnumerator enterSpaceZone()
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(4);
    }

    public void Update()
    {
        if (group.alpha == 0)
        {
            menuObject1.SetActive(true);
            menuObject2.SetActive(true);
            menuObject3.SetActive(true);
        }
        else
        {
            menuObject1.SetActive(false);
            menuObject2.SetActive(false);
            menuObject3.SetActive(false);
        }
    }
}
