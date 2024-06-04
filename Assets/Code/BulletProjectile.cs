using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitEffectSoft;
    [SerializeField] private Transform vfxHitEffectHard;
    [SerializeField] private TrailRenderer tracer;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 100f;
        body.velocity = transform.forward * speed;
        StartCoroutine(bulletDecay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AI_Enemy>() != null)
        {
            Instantiate(vfxHitEffectSoft, transform.position, Quaternion.identity);       
            Destroy(gameObject);
        }
        else if (other.GetComponent<ThirdPersonShooterContorller>() != null)
        {
            Instantiate(vfxHitEffectSoft, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(vfxHitEffectHard, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(vfxHitEffectHard, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator bulletDecay()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
