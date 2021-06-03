using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMeleeEnemyScript : MonoBehaviour
{
    public float enemySpeed;
    public float attackCooldown;
    public float distanceUntilMoving;
    public int enemyDamage;
    //public int enemyHealth;



    public float enemyHealth;
    public float maxhealth;

    private GameObject player;
    private float attackCooldownTemp;
    public ParticleSystem deathParticle;
    [SerializeField] private AudioClip deathSound;

    public GameObject healthBarUI;
    public Slider slider;
    private Animator anim; 
    void Start()
    {
        player = GameObject.Find("Player");

        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 1f);
    }

    void Update()
    {
        transform.LookAt(player.transform);

     

        if (attackCooldownTemp >= 0.0f)
        {
            attackCooldownTemp -= Time.deltaTime;
        }
        else
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceUntilMoving))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    transform.position += transform.forward * enemySpeed * Time.deltaTime;
                   
                }
            }
        }
    }

    public void Damage(int damage)
    {
        enemyHealth -= damage;
        slider.value = CalculateHealth();

        if (enemyHealth < maxhealth)
        {

            healthBarUI.SetActive(true);
        }



        if (enemyHealth <= 0)
        {

            DeathEnemy();
        }

        Debug.Log("Enemy: Health Left: " + enemyHealth);
    }

    private void DeathEnemy()
    {

        
        //healthbar.gameObject.SetActive(false);
        Instantiate(deathParticle, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.5f);
        StartCoroutine(KillEnemy());
    }

     private IEnumerator KillEnemy()
    {
        //gameObject.SetActive(false);
        transform.position = new Vector3(0, 3000, 0);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }




    float CalculateHealth()
    {
        return enemyHealth / maxhealth;
    }





    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetFloat("Speed", 2.0f);

            if (attackCooldownTemp <= 0.0f)
            {
                Debug.Log("Enemy: Hit Player");
                other.GetComponent<PlayerHealth>().Damage(enemyDamage);
                attackCooldownTemp = attackCooldown;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))

            anim.SetFloat("Speed", 1f);


    }


}
