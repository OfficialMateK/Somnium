using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyCrystal : MonoBehaviour
{
    //Health
    public float maxHealth = 500f;
    public float damageThreshold = 50f;

    //UI
    public CrystalUI healthUI;
    private UIChangeTrigger uiChangeTrigger;

    //private LockedDoor lockedDoor;
    private float currentHealth = 500f;
    public float healthRegenRate = 15f;
    private float currentDamageThreshold = 0;
    //private float invinciblePeriod = 0.1f;


    //Enemies
    public Transform[] spawnPoints;
    public GameObject meleeEnemyPrefab;
    public int EnemyBurstThreshold = 10;

    private bool canSpawnEnemies = false;
    private int enemiesToBurstSpawn = 0;
    private int currentEnemyBurstThreshold = 0;
    private int spawnPointNumber;
    private float enemySpawnTime = 10f;
    private float currentSpawnTime = 0f;

    private GameManager gameManager;


    private void Start()
    {
        //lockedDoor = GameObject.Find("LockedDoor").GetComponent<LockedDoor>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        uiChangeTrigger = GetComponentInChildren<UIChangeTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnEnemies)
        {
            healthUI.SetHealth(currentHealth);
            CheckHealth();
            SpawnEnemiesOverTime();
            SpawnEnemyBurst();
            RegenHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        //Vid skada - s�nker liv och �ker burst-threshold
        canSpawnEnemies = true;
        currentHealth -= damage;
        currentDamageThreshold += damage;
        currentEnemyBurstThreshold++;
        
        if(currentHealth <= 0)
        {
            canSpawnEnemies = false;
            CompleteObjective();
        }
    }

    private void CheckHealth()
    {
        //�kar eller s�nker antalet fiender som spawnar genom att kolla damageThreshold
        //damageThreshold �kar n�r spelaren g�r skada och s�nks �ver tid
        //N�r thresholden har g�tt �VER max startas den om fr�n 0 och om den g�r UNDER 0 s�nks den till max
        //Den skapar allts� en �ndring var 50:e health
        if(currentDamageThreshold > damageThreshold)
        {
            currentDamageThreshold -= damageThreshold;
            enemySpawnTime--;
            enemiesToBurstSpawn++;
        }
        else if(currentDamageThreshold < 0)
        {
            currentDamageThreshold = damageThreshold;
            enemySpawnTime++;
            enemiesToBurstSpawn--;
        }
    }

    private void SpawnEnemiesOverTime()
    {
        //Spawnar en fiende varje spawnTime-cykel
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime >= enemySpawnTime)
        {
            currentSpawnTime = 0f;
            SpawnEnemies();
        }
    }

    private void SpawnEnemyBurst()
    {
        //Spawnar en klump av fiender efter att tagit skada ett antal g�nger
        if(currentEnemyBurstThreshold >= EnemyBurstThreshold)
        {
            currentEnemyBurstThreshold = 0;
            for (int i = 0; i < enemiesToBurstSpawn; i++)
            {
                SpawnEnemies();
            }
        }
    }

    private void SpawnEnemies()
    {
        spawnPointNumber = Random.Range(0, spawnPoints.Length);
        GameObject meleeEnemy = Instantiate(meleeEnemyPrefab, spawnPoints[spawnPointNumber].position, spawnPoints[spawnPointNumber].rotation);
    }

    private void RegenHealth()
    {
        //N�r den inte har fullt liv �kas health med den healthRegenRate varje sekund
        if(currentHealth < maxHealth)
        {
            currentHealth += Time.deltaTime * healthRegenRate;
            currentDamageThreshold -= Time.deltaTime * healthRegenRate;
        }
    }

    private void CompleteObjective()
    {
        //lockedDoor.IncreaseObjectivesComplete();
        gameManager.CompleteCrystal();
        uiChangeTrigger.triggerUIChange(1);
        Destroy(gameObject, 0.1f);
    }
}
