using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public int levelNumber;
    public bool crystalCompleted;
    public bool collectionCompleted;
    public bool captureCompleted;

    public int health;
    public float[] position;
    public int ammoCurrent;
    public int ammoBackup;

    
    public PlayerSaveData(GameObject player, GameManager manager)
    {
        levelNumber = manager.GetSceneNumber();
        health = player.GetComponent<PlayerHealth>().health;
        //ammoCurrent = player.GetComponentInChildren<>
        //ammoBackup = player.GetComponentInChildren<>

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        crystalCompleted = manager.GetCrystalCompleted();
        collectionCompleted = manager.GetCollectionCompleted();
        captureCompleted = manager.GetCaptureCompleted();
    }
    

}
