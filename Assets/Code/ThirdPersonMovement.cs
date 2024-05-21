using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Game Scene Refrences
    [SerializeField] private HealthBar healthBar;
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheckMarker;
    public GameObject player;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public GameObject bullet;
    public Rigidbody bulletRB;
    public Animator animator;

    // Game Asset Values
    [SerializeField] private float _MaxHealth = 100f;
    private float _currentHealth;
    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public float bulletSpeed = 10;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    Vector3 velocity;
    bool isGrounded;
    bool isAimed;
    private void Awake()
    {
        _currentHealth = _MaxHealth;

        healthBar.UpdateHealthBar(_MaxHealth, _currentHealth);
    }

    void Update()
    {
        // Animation Update
        animator.SetBool("Fire", false);
        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Mine Hit", false);
        animator.SetBool("Stand", false);
        animator.SetBool("Hit", false);

        // Gravity
        isGrounded = Physics.CheckSphere(groundCheckMarker.position, groundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Movement Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // 2 Axis Movement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);
        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("Jump", true);
        }

        // Aim
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("Aim", true);
            isAimed = true;
        }
        else
        {
            animator.SetBool("Aim", false);
            isAimed = false;
        }

        //Static Fire
        if (isAimed && Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("Fire", true);
            Instantiate(muzzleFlash, firePoint.transform.position, firePoint.transform.rotation);
            Fire();
        }
        else
        {
            animator.SetBool("Fire", false);
        }

        //Animation
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    // Damage, Death & Respawn
    public void TakeDamageP(int damage)
    {
        _currentHealth -= damage;
        healthBar.UpdateHealthBar(_MaxHealth, _currentHealth);
        if (_currentHealth <= 0) Invoke(nameof(killPlayer), 0.5f);
    }

    void killPlayer()
    {
        StartCoroutine(respawn());
        GameObject.FindWithTag("player").SetActive(false);
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(5);
        GameObject.FindWithTag("player").SetActive(true);
        animator.SetBool("Stand", true);
        yield return new WaitForSeconds(1);
        StopCoroutine(respawn());
    }

    // Weapon Firing
    void Fire()
    {
        Rigidbody bulletClone = (Rigidbody)Instantiate(bulletRB, firePoint.transform.position, firePoint.transform.rotation);
        bulletClone.velocity = firePoint.transform.forward * bulletSpeed;
    }

    // Collision Variable
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mine")
        {
            animator.SetBool("Mine Hit", true);
            StartCoroutine(respawn());
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            animator.SetBool("Hit", true);
            TakeDamageP(5);
        }
    }
}
