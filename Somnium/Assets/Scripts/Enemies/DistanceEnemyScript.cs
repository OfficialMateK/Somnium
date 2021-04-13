using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemyScript : MonoBehaviour
{
    public float enemySpeed;
    public float attackCooldown;
    public float distanceUntilShoot;
    public float distanceUntilMoving;
    public float bulletSpeed;
    public int enemyDamage;
    public int enemyHealth;
    public Transform weaponPoint;
    public GameObject bulletPrefab;

    private GameObject player;
    private float attackCooldownTemp;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);

        if (attackCooldownTemp >= 0.0f)
        {
            attackCooldownTemp -= Time.deltaTime;
        } else
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceUntilMoving))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    transform.position += transform.forward * enemySpeed * Time.deltaTime;
                }
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceUntilShoot))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    Shoot();
                    attackCooldownTemp = attackCooldown;
                }
            }
        }
    }

    public void Damage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log("Enemy: Health Left: " + enemyHealth);
    }

    private void Shoot()
    {
        weaponPoint.LookAt(player.transform);

        GameObject bullet = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation) as GameObject;
        bullet.GetComponent<BulletScript>().bulletSpeed = bulletSpeed;
        bullet.GetComponent<BulletScript>().bulletDamage = enemyDamage;
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (attackCooldownTemp <= 0.0f)
    //        {
    //            Debug.Log("Enemy: Hit Player");
    //            other.GetComponent<PlayerHealth>().Damage(enemyDamage);
    //            attackCooldownTemp = attackCooldown;
    //        }
    //    }
    //}
}
