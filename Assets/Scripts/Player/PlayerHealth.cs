using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public HealthBar healthBar;

    private void Start()
    {
        //healthText.text = health.ToString();
        healthBar.SetMaxHealth(health);
    }

    public void Damage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        //  healthText.text = health.ToString();
        //Debug.Log("Player: Health Left = " + health);
    }
}
