using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool crystalCompleted;
    private bool collectionCompleted;
    private bool captureCompleted;
    private int sceneNumber;
    private LockedDoor lockedDoor;

    private void Start()
    {
        lockedDoor = GameObject.Find("LockedDoor").GetComponent<LockedDoor>();
    }

    public void CompleteCrystal()
    {
        crystalCompleted = true;
        CheckToOpenDoor();
    }

    public void CompleteCollection()
    {
        collectionCompleted = true;
        CheckToOpenDoor();
    }

    public void CompleteCapture()
    {
        captureCompleted = true;
        CheckToOpenDoor();
    }

    private void CheckToOpenDoor()
    {
        if(crystalCompleted && collectionCompleted && captureCompleted)
        {
            lockedDoor.UnlockDoor();
        }
    }

    public bool GetCrystalCompleted() {return crystalCompleted;}

    public bool GetCollectionCompleted() {return collectionCompleted;}

    public bool GetCaptureCompleted() {return captureCompleted;}

    public int GetSceneNumber() {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        return sceneNumber; 
    }

}
