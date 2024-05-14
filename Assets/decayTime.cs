using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decayTime : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(decay());
    }
    IEnumerator decay()
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
