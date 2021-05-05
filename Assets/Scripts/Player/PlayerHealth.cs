using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health;

    }
	
	void FixedUpdate()
	{
		HealthState();
	}
	
	public int GetHealth()
	{
		return health;
	}
	
	public void SetHealth(int setValue)
	{
		health = setValue;
	}

    public void Damage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        PlayerPrefs.SetInt("PlayerHealth", health);

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        //  healthText.text = health.ToString();
        //Debug.Log("Player: Health Left = " + health);
    }
	
	private void HealthState()
	{
		if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
