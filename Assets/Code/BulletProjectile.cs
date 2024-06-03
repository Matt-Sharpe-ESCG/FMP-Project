using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitEffectSoft;
    [SerializeField] private Transform vfxHitEffectHard;

    private Rigidbody body;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        body.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TargetScript>() != null)
        {
            Instantiate(vfxHitEffectSoft, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(vfxHitEffectHard, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
