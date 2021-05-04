using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    private GameObject[] distanceEnemies;
    private GameObject[] meleeEnemies;
    private GameObject[] newMeleeEnemies;
    private int intEnemies;
    public Text enemiesLeftText;
    void Update()
    {
        distanceEnemies = GameObject.FindGameObjectsWithTag("Distance Enemy");
        meleeEnemies = GameObject.FindGameObjectsWithTag("Melee Enemy");
        newMeleeEnemies = GameObject.FindGameObjectsWithTag("New Melee Enemy");

        intEnemies = distanceEnemies.Length + meleeEnemies.Length + newMeleeEnemies.Length;

        enemiesLeftText.text = "Enemies Left: " + intEnemies;
    }
}
