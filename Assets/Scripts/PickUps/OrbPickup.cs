using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickup : MonoBehaviour
{
    [SerializeField] private CollectOrbs collectOrbs;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private AudioClip orbSound;

    private void Start()
    {
        collectOrbs = GetComponentInParent<CollectOrbs>();
        gameObject.SetActive(false);
    }

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
        collectOrbs.PickupOrb();

        //Destroy object
        Destroy(gameObject);

    }



}
