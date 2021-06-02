using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    //public int level;
    //public bool crystalCompleted;
    //public bool collectionCompleted;
    //public bool captureCompleted;

    public float health;
    public float[] position;
    public int ammoCurrent;
    public int ammoBackup;

    public PlayerSaveData(CharacterControllerJohan player)
    {
        health = player.GetComponent<PlayerHealth>().health;
        //ammoCurrent = player.GetComponentInChildren<>
    }

}
