using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    // Animator
    public Animator animator;

    // Int Variable
    public int transitionTime = 1;

    public void playGameTrigger()
    {
        StartCoroutine(playGame(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator playGame(int gameIndex)
    {
        animator.SetTrigger("Transition");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(gameIndex);
    }

    public void loadMainMenu()
    {
        StartCoroutine(loadMenu(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator loadMenu(int gameIndex)
    {
        animator.SetTrigger("Transition");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(gameIndex);
    }

    public void quitGameTrigger()
    {
        StartCoroutine(quitGame());
    }

    IEnumerator quitGame()
    {
        animator.SetTrigger("Transition");

        yield return new WaitForSeconds(transitionTime);

        Application.Quit();
    }

    public void playTransition()
    {
        animator.SetTrigger("Transition");
    }
}
