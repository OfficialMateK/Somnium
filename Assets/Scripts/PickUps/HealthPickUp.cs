using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickUp : MonoBehaviour
{

    PlayerHealth playerHealth;



    public float healthBonus = 15f;


    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (playerHealth.curHealth < playerHealth.health)
        {
            Destroy(gameObject);
            playerHealth.curHealth = playerHealth.curHealth + healthBonus;
        }
    }
}
