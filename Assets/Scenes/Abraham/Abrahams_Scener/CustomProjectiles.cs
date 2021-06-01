using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Thanks for downloading my custom projectiles script! :D
/// Feel free to use it in any project you like!
/// 
/// The code is fully commented but if you still have any questions
/// don't hesitate to write a yt comment
/// or use the #coding-problems channel of my discord server
/// 
/// Dave

public class CustomProjectiles : MonoBehaviour
{
    public bool activated;

    [Header("Please attatch components:")]
    public Rigidbody rb;
    public GameObject explosion;
    public int bulletDamage;
    public LayerMask whatIsEnemies;

    [Header("Set the basic stats:")]
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    [Header("Explosion:")]
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    [Header("Lifetime:")]
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    [Header("Second bullets (Spawn after explosion)")]
    public GameObject secondBullet;
    public int sb_amount;
    public float sb_forwardForce, sb_upwardForce, sb_randomForce;

   

  

    

   
   

    


    private int collisions;

    private PhysicMaterial physic_mat;
    public bool alreadyExploded;

    /// Call the setup and attribute functions that need to be called directly at the start
    /// as well as set some variables
    void Start()
    {
        Setup();

        
        
    }

    /// Here are all functions called (except Setup), it works always the same,
    /// check if a specific requirement is fullfilled and if so, call the function
    void Update()
    {
        if (!activated) return;

        if (collisions >= maxCollisions && activated) Explode();

        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0 && activated) Explode();

       
        


    }

    ///Just to set the basic variables of the bullet/projectile
    private void Setup()
    {
        //Setup physics material
        physic_mat = new PhysicMaterial();
        physic_mat.bounciness = bounciness;
        physic_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physic_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Apply the physics material to the collider
        GetComponent<SphereCollider>().material = physic_mat;

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

        //Check for enemies and damage them
       

        //Invoke destruction
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<TrailRenderer>().emitting = false;
        Invoke("Delay", 0.08f);

        
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
