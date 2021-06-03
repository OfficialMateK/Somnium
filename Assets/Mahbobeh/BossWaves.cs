using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaves : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private int enemiesAlive = 2;
    private int enemiesToSpawn = 2;
    private float waveCooldown = 5f;
    private bool startCooldown = false;

    [SerializeField] private Transform[] spawnPoints;
    private int spawnPointIndex;

    [SerializeField] private GameObject AccomplishOrb;

    private void Update()
    {
        if (startCooldown)
        {
            waveCooldown -= Time.deltaTime;

            if (waveCooldown <= 0)
            {
                waveCooldown = 5f;
                SpawnEnemies();
                startCooldown = false;
            }
        }

    }

    private void SpawnEnemies()
    {
        if(enemiesToSpawn == 8)
        {
            AccomplishOrb.SetActive(true);
        }
        else
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                spawnPointIndex = Random.Range(0, spawnPoints.Length);
                GameObject meleeEnemy = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            }



        }


        
    }

    public void DecreaseEnemies()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            enemiesToSpawn += 2;
            enemiesAlive = enemiesToSpawn;
            StartWaveCooldown();
        }
    }

    private void StartWaveCooldown()
    {
        startCooldown = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartWaveCooldown();
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
