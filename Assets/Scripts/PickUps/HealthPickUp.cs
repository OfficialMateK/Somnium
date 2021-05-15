using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickUp : MonoBehaviour
{
    public int healthBonus = 15;

    public AudioClip heartSound;
    public GameObject pickupEffect;

    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().AddHealth(healthBonus);
            
            pickUp();
            
        }
    }

    void pickUp()
    {
        //Spawn a effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        AudioSource.PlayClipAtPoint(heartSound, transform.position);

        Destroy(gameObject);
    }


}
