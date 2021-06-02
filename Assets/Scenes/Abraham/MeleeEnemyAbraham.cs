using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MeleeEnemyAbraham : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask Ground, Player;
    public Animator animator;

    public ParticleSystem deathParticle;
    // [SerializeField] private AudioClip deathSound;
    // [SerializeField] private AudioClip shootSound;

    //public GameObject healthBarUI;
    //public Slider slider;
    public float enemyHealth;
    public float maxhealth;
    public int enemyDamage;



    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
   

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //healthbar = GetComponentInChildren<UIHealthBar>();
        enemyHealth = maxhealth;
        //slider.value = CalculateHealth();
    }

    private void Update()
    {

        //slider.value = CalculateHealth();

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
     // if (playerInAttackRange && playerInSightRange) AttackPlayer();


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

       animator.SetFloat("Speed", 0);

      }

    private void SearchWalkPoint()
    {

    }


    private void ChasePlayer()
    {

        transform.LookAt(player);

        agent.SetDestination(player.position);
        if (!alreadyAttacked)
        {

            
        }



        animator.SetFloat("Speed",1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
                Debug.Log("Enemy: Hit Player");
                other.GetComponent<PlayerHealth>().Damage(enemyDamage);
                
            
        }
    }




    // private void AttackPlayer(Collider other)
    // {
    //     //Make sure enemy doesent move
    //     agent.SetDestination(transform.position);
    //
    //     transform.LookAt(player);
    //
    //     if (other.gameObject.CompareTag("Player"))
    //    {
    //         other.GetComponent<PlayerHealth>().Damage(enemyDamage);

    //     }




    // }



    private void ResetAttack()
    {
        alreadyAttacked = false;


    }


    public void Damage(int damage)
    {
        enemyHealth -= damage;
        //slider.value = CalculateHealth();

        if (enemyHealth < maxhealth)
        {

            //healthBarUI.SetActive(true);
        }



        if (enemyHealth <= 0)
        {

            KillEnemy();
        }

        Debug.Log("Enemy: Health Left: " + enemyHealth);
    }

    private void KillEnemy()
    {

        Destroy(gameObject);
        //healthbar.gameObject.SetActive(false);
        //Instantiate(deathParticle, transform.position, transform.rotation);
        //AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.5f);
    }

    float CalculateHealth()
    {
        return enemyHealth / maxhealth;
    }


}
