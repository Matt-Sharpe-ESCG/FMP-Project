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
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform vfxHitEffectSoft;
    [SerializeField] private Transform vfxHitEffectHard;
    [SerializeField] private ParticleSystem cases;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private Transform casesSpawn;
    [SerializeField] private float magezineSize = 16;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float _MaxHealth = 100f;
    private float ammoCount;
    private float _currentHealth;
    private ThirdPersonController thirdPerson;
    private StarterAssetsInputs starterAssetsInputs;
    public OtherAudioManager newAudioManager;
    private Animator animator;
    private bool isAimed;

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
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (starterAssetsInputs.aim)
        {
            isAimed = true;
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPerson.SetSensitivity(aimSensitvity);
            thirdPerson.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            Quaternion quaternion = Quaternion.Euler(worldAimTarget.x, aimDirection.y, worldAimTarget.z);

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(5, transform.rotation.y, transform.rotation.z);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPerson.SetSensitivity(normalSensitvity);
            thirdPerson.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            isAimed = false;
        }

        /*
           Vector3 mouse_pos;
           Transform target = null; //Assign to the object you want to rotate
           Vector3 object_pos;
           float angle;
           mouse_pos = Input.mousePosition;
           mouse_pos.z = 5.23f; //The distance between the camera and object
           object_pos = Camera.main.WorldToScreenPoint(target.position);
           mouse_pos.x = mouse_pos.x - object_pos.x;
           mouse_pos.y = mouse_pos.y - object_pos.y;
           angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
           target.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0)); 

           Vector3 mousePos = Input.mousePosition;
           mousePos.z = -(transform.position.x - Camera.mainCamera.transform.position.x);

           Vector3 worldPos = Camera.mainCamera.ScreenToWorldPoint(mousePos);

           transform.LookAt(worldPos);

           var mouse_pos : Vector3;
            var target : Transform; //Assign to the object you want to rotate
            var object_pos : Vector3;
            var angle : float;

            function Update ()
            {
                mouse_pos = Input.mousePosition;
	            mouse_pos.z = 5.23; //The distance between the camera and object
	            object_pos = Camera.main.WorldToScreenPoint(target.position);
	            mouse_pos.x = mouse_pos.x - object_pos.x;
	            mouse_pos.y = mouse_pos.y - object_pos.y;
	            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
	            transform.rotation = Quaternion.Euler(Vector3(0, 0, angle));
            }
        */

        if (starterAssetsInputs.shoot && isAimed == true && ammoCount > 0)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;       
            Instantiate(vfxHitEffectHard, spawnBulletPosition.transform.position, spawnBulletPosition.transform.rotation);
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            newAudioManager.PlaySFX(newAudioManager.gunShot[0]);
            ammoCount = ammoCount - 1;
            starterAssetsInputs.shoot = false;
            
        }

        if (starterAssetsInputs.reload)
        {
            animator.SetBool("Reload", true);
            ammoCount = magezineSize;
        }
    }

    public void TakeDamageP(int damage)
    {
        _currentHealth -= damage;
        healthBar.UpdateHealthBar(_MaxHealth, _currentHealth);
        if (_currentHealth == 0)
        {
            killPlayer();
        }
    }

    void killPlayer()
    {
        gameObject.SetActive(false);
    }
}
