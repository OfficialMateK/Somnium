using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public Text healthText;
    public float curHealth; //For health pick up script


    private void Start()
    {
        healthText.text = health.ToString();
        curHealth = health; //For health pick up script
    }

    public void Damage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        healthText.text = health.ToString();
        Debug.Log("Player: Health Left = " + health);
    }
}
