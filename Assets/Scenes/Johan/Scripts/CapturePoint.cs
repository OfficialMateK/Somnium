using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    public float captureTimeGoal = 60f;
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public Transform[] spawnPoints;

    private float currentCaptureTime = 0f;
    private bool hasBeenStarted = false;
    private bool hasBeenCompleted = false;
    private bool playerInside = false;

    private float numberOfEnemiesInside = 0;
    private int numberOfEnemiesToSpawn;
    private float spawnTime = 0f;
    private int spawnPointNumber;


    void Update()
    {
        if (hasBeenStarted && !hasBeenCompleted)
        {
            CheckCaptureState();
            SpawnEnemies();
        }
        else if (hasBeenCompleted)
        {
            CompleteCapture();
        }
       
    }

    private void CheckCaptureState()
    {
        if(currentCaptureTime < captureTimeGoal)
        {
            if (playerInside && numberOfEnemiesInside == 0)
            {
                StartCaptureCount();
            }
            else
            {
                StopCaptureCount();
            }
        }
        else
        {
            hasBeenCompleted = true;
        }

    }

    private void StartCaptureCount()
    {
        currentCaptureTime += Time.deltaTime;
        //Debug.Log("Current time: " + currentCaptureTime);

    }

    private void StopCaptureCount()
    {
        Debug.Log("Stopped time at:" + currentCaptureTime);
    }

    private void SpawnEnemies()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime >= 10f)
        {
            spawnTime = 0f;
            numberOfEnemiesToSpawn = Random.Range(4, 7);

            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                spawnPointNumber = Random.Range(0, spawnPoints.Length);
                GameObject meleeEnemy = Instantiate(meleeEnemyPrefab, spawnPoints[spawnPointNumber].position, spawnPoints[spawnPointNumber].rotation);
            }
        }
    }

    private void CompleteCapture()
    {
        //GameController.objectivesCompleted++;
        //*Cool particles*
        Debug.Log("Capture complete: " + currentCaptureTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasBeenStarted = true;
            playerInside = true;
        }

        if (other.CompareTag("Melee Enemy") || other.CompareTag("Distance Enemy"))
        {
            numberOfEnemiesInside++; ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;   
        }

        if (other.CompareTag("Melee Enemy") || other.CompareTag("Distance Enemy"))
        {
            numberOfEnemiesInside--;
        }
    }
}
