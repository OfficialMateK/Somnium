using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMeleeEnemyScript : MonoBehaviour
{
    public float enemySpeed;
    public float attackCooldown;
    public float distanceUntilMoving;
    public int enemyDamage;
    public int enemyHealth;

    private GameObject player;
    private float attackCooldownTemp;
    private Animator anim; 
    void Start()
    {
        player = GameObject.Find("Player");

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(player.transform);

        if (attackCooldownTemp >= 0.0f)
        {
            attackCooldownTemp -= Time.deltaTime;
        }
        else
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceUntilMoving))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    transform.position += transform.forward * enemySpeed * Time.deltaTime;
                    anim.SetBool("walkForward", true); 
                }
            }
        }
    }
    public void Damage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            StartCoroutine(KillEnemy());
        }

        Debug.Log("Enemy: Health Left: " + enemyHealth);
    }

    private IEnumerator KillEnemy()
    {
        //gameObject.SetActive(false);
        transform.position = new Vector3(0, 3000, 0);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (attackCooldownTemp <= 0.0f)
            {
                Debug.Log("Enemy: Hit Player");
                other.GetComponent<PlayerHealth>().Damage(enemyDamage);
                attackCooldownTemp = attackCooldown;
            }
        }
    }
}
