using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickup : MonoBehaviour
{

    public GameObject pickupEffect;
    public AudioClip orbSound;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }


    }

    void Pickup()
    {

        //Spawn a effect
        Instantiate(pickupEffect, transform.position, transform.rotation);


        AudioSource.PlayClipAtPoint(orbSound, transform.position);

        //Destroy object
        Destroy(gameObject);

    }



}
