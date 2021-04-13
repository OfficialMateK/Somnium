using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public Transform sp1, sp2, sp3, sp4;
    public GameObject finalDoor;

    private int keyLocation;
    private int newLocation;
    private int timesPickedUp;
    void Start()
    {
        timesPickedUp = 0;
        keyLocation = Random.Range(1, 5);

        switch(keyLocation)
        {
            case 1:
                transform.position = sp1.position;
                break;
            case 2:
                transform.position = sp2.position;
                break;
            case 3:
                transform.position = sp3.position;
                break;
            case 4:
                transform.position = sp4.position;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            timesPickedUp++;

            if(timesPickedUp == 3)
            {
                finalDoor.GetComponent<ChangeLevelScript>().enabled = true;
                Destroy(gameObject);
            } else
            {
                PlaceKey();
            }
        }
    }
    private void PlaceKey()
    {
        newLocation = Random.Range(1, 5);

        while(newLocation == keyLocation)
        {
            newLocation = Random.Range(1, 5);
        }

        switch (newLocation)
        {
            case 1:
                transform.position = sp1.position;
                break;
            case 2:
                transform.position = sp2.position;
                break;
            case 3:
                transform.position = sp3.position;
                break;
            case 4:
                transform.position = sp4.position;
                break;
            default:
                break;
        }
    }
}
