using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public float timetodie = 10.0f;
    public int bulletDamage;

    private float bulletVelocity;
    private Vector3 bulletDirection;

    private void Start()
    {

    }
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        timetodie -= Time.deltaTime;
        if(timetodie <= 0.0f)
        {
            Debug.Log("Auto Destruction!");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Melee Enemy":
                collision.gameObject.GetComponent<MeleeEnemyScript>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "Distance Enemy":
                collision.gameObject.GetComponent<DistanceEnemyScript>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "Player":
                collision.gameObject.GetComponent<PlayerHealth>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "Ignore":
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
}
