using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{
    public GameObject explosion;
    public ThirdPersonShooterContorller ThirdPM;
    public AI_Enemy Enemy;
    public Collider Collision;
    public OtherAudioManager OtherAudio;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            ThirdPM.TakeDamageP(50);
            OtherAudio.PlaySFX(OtherAudio.mineExplostion);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Enemy.TakeDamage(50);
            Destroy(gameObject);
            OtherAudio.PlaySFX(OtherAudio.mineExplostion);
        }
    }
}
