using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MeleeEnemy : MonoBehaviour
{
    public Transform player;
    public LayerMask Player;
    private Animator animator;
    //public UIHealthBar health;
    public NavMeshAgent agent;
    //public GameObject healthBarUI;
    //public ParticleSystem deathParticle;
    //public Slider slider;
    public float enemyHealth;
    public float maxHealth;

    //states

    public Vector3 walk;
    bool walkSet;
    public float walkRange;

    public float coolDown;
    public GameObject warHammer;
    bool attackedByEnemy;

    public float sight, attack;
    public bool playerInSight, playerInAttack;

   

    public void Awake()
    {
        agent = GetComponent <NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        //health = GetComponentInChildren<UIHealthBar>();
        enemyHealth = maxHealth;
        //slider.value = CalculateHealth(); 
    }

    private void Update()
    {
        //slider.value = CalculateHealth();

        playerInSight = Physics.CheckSphere(transform.position, sight, Player);
        playerInAttack = Physics.CheckSphere(transform.position, attack, Player);

        if (playerInSight && !playerInAttack) EnemyChase();
        if (playerInAttack && playerInSight) EnemyAttack();
  


    }

    private void EnemyChase()
    {
        transform.LookAt(player);

        agent.SetDestination(player.position);
        //if (!attackedByEnemy)
        //{
            //Rigidbody rigbod = Instantiate(warHammer).GetComponent<Rigidbody>();
            //rigbod.AddForce(transform.forward * 35, ForceMode.Impulse);
            //attackedByEnemy = true;
            //Invoke(nameof(ResetEnemyAttack), coolDown);
        //}

        animator.SetFloat("Speed", 0.6f);
    }

    private void EnemyAttack()
    {   
        
        transform.LookAt(player);
        
        agent.SetDestination(player.position);
        //if (!attackedByEnemy)
        //{
            //Rigidbody rigbod = Instantiate(warHammer).GetComponent<Rigidbody>();
            //rigbod.AddForce(transform.forward * 35, ForceMode.Impulse);
            //attackedByEnemy = true;
            //Invoke(nameof(ResetEnemyAttack), coolDown);
            animator.SetFloat("Speed", 1f);
        //}

        
    }

    private void ResetEnemyAttack()
    {
        attackedByEnemy = false;
    }

    public void Injury(int damage)
    {
        enemyHealth -= damage;
        //slider.value = CalculateHealth();

        if (enemyHealth < maxHealth)
        {
            //healthBarUI.SetActive(true);
        }

        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }

        Debug.Log("Health Left: " + enemyHealth);
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
        //Instantiate(deathParticle, transform.position, transform.rotation);
    } 
    float CalculateHealth()
    {
        return enemyHealth / maxHealth;
    }

}


