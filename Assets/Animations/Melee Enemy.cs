using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    public UIhealthBar health;
    public float enemyHealth;
    public float maxHealth;

    public Vector3 walk;
    bool walkSet;
    public float walkRange;

    public float coolDown;
    public GameObject warHammer;

    public float sight, attack;
    public bool playerInSight, playerInAttack;

    void Start ()
    {
        player = GameObject.Find("Player").transform;

        anim = GetComponent<Animator>();
    }

    public void Awake()
    {
        if (!enemydeath)
        {
            nav = GetComponent <UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        health = GetComponentInChildren<UIHealthBar>();
        enemyHealth = maxHealth;
        }
        
    }

    public void Death() 
    {
        enemydeath = true;
        nav.Stop();
        anim.SetTrigger("Death");
        Destroy(this.gameObject, 10.0f);
        health.gameObject.SetActive(false);
        Instantiate(deathParticle, transform.position, transform.rotation);

    
    }
}
