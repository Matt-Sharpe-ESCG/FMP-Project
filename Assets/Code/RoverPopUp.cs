using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverPopUp : MonoBehaviour
{
    public GameObject roverTrigger;
    public GameObject roverUI;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            roverUI.SetActive(true);
        }
    }
}
