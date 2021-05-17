using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public HealthBar healthBar;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //healthText.text = health.ToString();

        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            if (PlayerPrefs.GetInt("PlayerHealth") <= 0)
            {
                PlayerPrefs.SetInt("PlayerHealth", health);
            }
            else
            {
                health = PlayerPrefs.GetInt("PlayerHealth");
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerHealth", health);
        }

        healthBar.SetMaxHealth(health);
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

        anim.SetTrigger("Hurt"); //player hurt animation

        //  healthText.text = health.ToString();
        //Debug.Log("Player: Health Left = " + health);
    }

    public void AddHealth(int healthToAdd)
    {
        if(health + healthToAdd > 200)
        {
            health = 200;
        } else
        {
            health += healthToAdd;
        }

        PlayerPrefs.SetInt("PlayerHealth", health);
        healthBar.SetHealth(health);
    }

    public void SetHealth(int setValue)
    {
        health = setValue;

        healthBar.SetHealth(health);
        PlayerPrefs.SetInt("PlayerHealth", health);

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
