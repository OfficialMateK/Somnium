using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickup : MonoBehaviour
{

    public GameObject pickupEffect;



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




        //Destroy object
        Destroy(gameObject);

    }



}
