using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulleans : MonoBehaviour
{
    public Rigidbody RB;
    public float bulletSpeed;
    public Vector3 velocity;
    public float decayTimer;
    ThirdPersonMovement ThirdPM;
    AI_Enemy Enemy;

    private void Awake()
    {
        RB = GetComponent <Rigidbody>();
        StartCoroutine(Decay());
    }

    public void Update()
    {
        StartCoroutine(Decay());
    }

    IEnumerator Decay()
    {
        yield return new WaitForSeconds(decayTimer);
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {       
         if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Enemy.TakeDamage(5);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
