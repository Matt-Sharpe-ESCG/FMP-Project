using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{
    public GameObject explosion;
    public ThirdPersonMovement ThirdPM;
    public AI_Enemy Enemy;
    public Collider Collision;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            ThirdPM.TakeDamageP(50);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Enemy.TakeDamage(50);
            Destroy(gameObject);
        }
    }
}
