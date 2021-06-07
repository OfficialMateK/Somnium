using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public float enemyHealth;
    public float currentHealth;
    //public float damage = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Damage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            KillEnemy();
        }

        Debug.Log("Enemy: Health Left: " + enemyHealth);
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
