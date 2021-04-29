using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Text healthText;

    private void Start()
    {
        healthText.text = health.ToString();
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

        healthText.text = health.ToString();
        Debug.Log("Player: Health Left = " + health);
    }
	
	private void HealthState()
	{
		if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
