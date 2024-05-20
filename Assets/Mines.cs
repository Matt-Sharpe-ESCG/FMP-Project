using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{
    public GameObject explosion;
    public ThirdPersonMovement ThirdPM;
    public AI_Enemy Enemy;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, transform);
            ThirdPM.TakeDamageP(20);
            Destroy(explosion);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, transform);
            Enemy.TakeDamage(20);
            Destroy(explosion);
        }
    }
}
