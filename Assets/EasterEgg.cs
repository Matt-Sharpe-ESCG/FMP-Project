using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    public Animator animator;

    public int transitionTime = 5;

    public void playMortimer()
    {
        
        StartCoroutine(mortimerPlay());
    }

    IEnumerator mortimerPlay()
    {
        animator.SetTrigger("Mortimer");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(5);
    }
}
