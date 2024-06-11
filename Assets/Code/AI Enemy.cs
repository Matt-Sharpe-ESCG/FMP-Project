using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    // Health Bar refrences
    [SerializeField] private float _MaxHealth = 50f;
    [SerializeField] private float _currentHealth;
    [SerializeField] private HealthBar healthBar;

    // Game Scene References
    public OtherAudioManager otherAudioManager;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;

    // Float, Bool & Int Variables
    public float detectionRadius = 10f;
    public float bulletSpeed = 50f;
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    // Set-Up Health Bar
    private void Awake()
    {
        _currentHealth = _MaxHealth;
        healthBar.UpdateHealthBar(_MaxHealth, _currentHealth);
    }

    // Gun and Bullet References
    public GameObject muzzleFlash;
    public GameObject bullet;
    public Transform firePoint;
    public Rigidbody bulletRB;

    // Game Object References
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Health Connections & Death
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


    //public NavMeshAgent agent;


    public LayerMask whatIsGround, whatIsPlayer;
  
    public Vector3 walkPoint;
    public LayerMask groundMask;
    Vector3 velocity;
    public float groundDistance = 0.4f;
    bool walkPointSet;
    public float walkPointRange;

// Movement Code Test 1
    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < detectionRadius)
        {
            agent.SetDestination(player.position);
            if (distance <= agent.stoppingDistance)
            {
                AttackPlayer();
                FaceTarget();
            }
        }

        if (Vector3.Magnitude(transform.position) > 1f)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Visualise Detection Radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

// Current System (Not Functional)
    /*
        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();

            if (Vector3.Magnitude(transform.position) > 1f)
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
    */
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

