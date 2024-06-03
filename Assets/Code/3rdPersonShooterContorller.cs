using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterContorller : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private float normalSensitvity;
    [SerializeField] private float aimSensitvity;
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform vfxHitEffectSoft;
    [SerializeField] private Transform vfxHitEffectHard;
    [SerializeField] private ParticleSystem cases;
    [SerializeField] private TrailRenderer trail;

    private ThirdPersonController thirdPerson;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    public AudioClip GunShot;

    private void Awake()
    {
        thirdPerson = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Transform hitTransform = null;
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPerson.SetSensitivity(aimSensitvity);
            thirdPerson.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPerson.SetSensitivity(normalSensitvity);
            thirdPerson.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (starterAssetsInputs.shoot)
        {
            Instantiate(trail, spawnBulletPosition.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(GunShot, transform.position);
            if (hitTransform != null)
            {
                Instantiate(vfxHitEffectSoft, hitTransform.position, Quaternion.identity);
            }        
            starterAssetsInputs.shoot = false;
        }
    }
}
