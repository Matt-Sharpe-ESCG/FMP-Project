using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    [SerializeField] private float _MaxHealth = 50f;
    private float _currentHealth;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public OtherAudioManager otherAudioManager;
    public GameObject bullet;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public Animator animator;
    public Rigidbody bulletRB;
    public Vector3 walkPoint;
    public Transform groundCheckMarker;
    public float gravity = -9.81f;
    public LayerMask groundMask;
    Vector3 velocity;
    public float groundDistance = 0.4f;
    bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float bulletSpeed;

    [SerializeField] private HealthBar healthBar;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        _currentHealth = _MaxHealth;

        healthBar.UpdateHealthBar(_MaxHealth, _currentHealth);
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        if (Vector3.Magnitude(transform.position) > 0f)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }

        animator.SetBool("Hit", false);
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        transform.rotation = Quaternion.LookRotation(walkPoint);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        animator.SetBool("Fire", false);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Debug.Log("Attack");
            animator.SetBool("Fire",true);
            animator.SetBool("move", false);

            Instantiate(muzzleFlash, firePoint.transform.position, firePoint.transform.rotation);
            otherAudioManager.PlaySFX(otherAudioManager.gunShot[1]);
            Fire();

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        healthBar.UpdateHealthBar(_MaxHealth, _currentHealth);
        if (_currentHealth <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void Fire()
    {
        Rigidbody bulletClone = (Rigidbody)Instantiate(bulletRB, firePoint.transform.position, firePoint.transform.rotation);
        bulletClone.velocity = firePoint.transform.forward * bulletSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullets")
        {
            animator.SetBool("Hit", true);
            TakeDamage(15);
        }
    }
}

