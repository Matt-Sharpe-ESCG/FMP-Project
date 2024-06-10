using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zelensky : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        source.clip = clip;
        source.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        source.Stop();
    }
}
