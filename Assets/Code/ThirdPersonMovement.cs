using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Game Scene Refrences
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheckMarker;
    public GameObject rover;
    public GameObject player;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public GameObject bullet;
    public Animator animator;

    // Game Asset Values
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    Vector3 velocity;
    bool isGrounded;
    bool isAimed;
    float health = 40f;

    void Update()
    {
        // Animation Update
        animator.SetBool("Fire", false);
        animator.SetBool("Run", false);
        animator.SetBool("Jump", false);

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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("Jump", true);
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.Return) && isAimed == true)
        {
            animator.SetBool("Fire", true);
            Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
            Instantiate(bullet, firePoint.position, firePoint.rotation);
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

        //Animation
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        // Running
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            speed = speed + 3f;
            animator.SetBool("Run", true);
        }
        else
        {
            speed = 6f;
        }
    }

    // Damage
    public void TakeDamageP(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(killPlayer), 0.5f);
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
    }
}
