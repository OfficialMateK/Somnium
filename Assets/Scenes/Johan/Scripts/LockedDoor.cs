using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{

    private int objectivesComplete = 0;


    private void UnlockDoor()
    {
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
