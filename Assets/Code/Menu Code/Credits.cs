using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public CanvasGroup group;
    public GameObject creditsObject;
    public GameObject menuObject;

    private void Update()
    {
        if (group.alpha == 0)
        {
            creditsObject.SetActive(false);
            menuObject.SetActive(true);
        }
    }
}
