using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceTransport : MonoBehaviour
{
    public GameObject transport;
    public GameObject UItransport;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            UItransport.SetActive(true);
        }
    }
}
