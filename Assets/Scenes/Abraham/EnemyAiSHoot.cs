using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiSHoot : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform weaponPoint;
    public LayerMask Ground, Player;
    public Animator animator;
    public UIHealthBar healthbar;
    public ParticleSystem deathParticle;


    public float enemyHealth;
    public float maxhealth;



    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

  

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        healthbar = GetComponentInChildren<UIHealthBar>();
        enemyHealth = maxhealth;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();


    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        animator.SetFloat("Speed", agent.velocity.magnitude);

    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }


    private void ChasePlayer()
    {

        transform.LookAt(player);

        agent.SetDestination(player.position);
        if (!alreadyAttacked)
        {

            //Attack code
            Rigidbody rb = Instantiate(projectile, weaponPoint.position, weaponPoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 50f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }



        animator.SetFloat("Speed", agent.velocity.magnitude);
    }


    private void AttackPlayer()
    {
        //Make sure enemy doesent move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            //Attack code
             Rigidbody rb = Instantiate(projectile, weaponPoint.position, weaponPoint.rotation).GetComponent<Rigidbody>();
             rb.AddForce(transform.forward * 80f, ForceMode.Impulse);
           

            //GameObject bullet = Instantiate(projectile, weaponPoint.position, weaponPoint.rotation) as GameObject;
            animator.SetFloat("Speed", 0);



            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }


    }



    private void ResetAttack()
    {
        alreadyAttacked = false;


    }


    public void Damage(int damage)
    {
        enemyHealth -= damage;
        healthbar.SetHealthBarPercentage(enemyHealth/maxhealth);

        if (enemyHealth <= 0)
        {
           KillEnemy();
        }

        Debug.Log("Enemy: Health Left: " + enemyHealth);
    }

    private void KillEnemy()
    {
        
        Destroy(gameObject);
        healthbar.gameObject.SetActive(false);
        Instantiate(deathParticle, transform.position, transform.rotation);
    }





}
