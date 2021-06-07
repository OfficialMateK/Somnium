using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomProjectiles : MonoBehaviour
{
    public bool activated;

    public Rigidbody rb;
    public GameObject explosion;
    public int bulletDamage;
    public LayerMask whatIsEnemies;

    public float bounciness;
    public bool useGravity;

   


   


   

    private PhysicMaterial physic_mat;
    public bool alreadyExploded;

    
    void Start()
    {
        Setup();    
    }

    
    void Update()
    {
        if (!activated) return;

    }

    ///Just to set the basic variables of the bullet/projectile
    private void Setup()
    {
        

        //Don't use unity's gravity, we made our own (to have more control)
        rb.useGravity = useGravity;
    }

    public void Explode()
    {
        //Bug fixing
        if (alreadyExploded) return;
        alreadyExploded = true;

        Debug.Log("Explode");

        //Instantiate explosion if attatched
        if (explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);
    
    }
    private void Delay()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Melee Enemy":
                collision.gameObject.GetComponent<MeleeEnemyScript>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "New Melee Enemy":
                collision.gameObject.GetComponent<NewMeleeEnemyScript>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "Distance Enemy":
                collision.gameObject.GetComponent<DistanceEnemyScript>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "Boss":
                collision.gameObject.GetComponent<EnemyAiSHoot>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "ShootingEnemy":
                collision.gameObject.GetComponent<DistanceEnemyV2>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "OgreEnemy":
                collision.gameObject.GetComponent<MeleeEnemyAbraham>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "Player":
                collision.gameObject.GetComponent<PlayerHealth>().Damage(bulletDamage);
                Destroy(gameObject);
                break;
            case "DestroyCrystal":
                collision.gameObject.GetComponent<DestroyCrystal>().TakeDamage(bulletDamage);
                Destroy(gameObject);
                break;
            case "Ignore":
                break;
            default:
                Destroy(gameObject);
                break;
        }
        //Explode on touch
        Explode();

        Destroy(gameObject, 1);
      
    }

   
    

   
   

  
}
