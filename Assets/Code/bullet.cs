using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public Rigidbody rb;
    AI_Enemy enemy;
    ThirdPersonMovement play;
    public float speed;
    public float decayTiming;
    public GameObject gameObjectFlash;

    void Start()
    {       
        StartCoroutine(decayTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            DamageP();
        }

        if (collision.CompareTag("Enemy"))
        {
            DamageE();
        }
    }

    void DamageE()
    {
        enemy.TakeDamage(5);
    }

    void DamageP()
    {
        play.TakeDamageP(5);
    }

    IEnumerator decayTime()
    {
        Destroy(gameObjectFlash);

        yield return new WaitForSeconds(decayTiming);

        Destroy(gameObject);
    }
}
