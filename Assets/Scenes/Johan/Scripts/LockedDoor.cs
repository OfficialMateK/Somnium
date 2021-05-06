using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{

    private int objectivesComplete = 0;
    public GameObject lightPillar;


    private void UnlockDoor()
    {
        lightPillar.SetActive(true);
        gameObject.SetActive(false);
    }

    public void IncreaseObjectivesComplete()
    {
        objectivesComplete++;
        if(objectivesComplete >= 3)
        {
            UnlockDoor();
        }
    }
}
