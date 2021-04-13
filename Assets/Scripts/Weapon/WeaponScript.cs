using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletDamage;
    public float speedOfBullets;
    public Transform weaponPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                weaponPoint.LookAt(hit.point);

                GameObject bullet = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation) as GameObject;
                bullet.GetComponent<BulletScript>().bulletSpeed = speedOfBullets;
                bullet.GetComponent<BulletScript>().bulletDamage = this.bulletDamage;
            } else
            {
                weaponPoint.LookAt(transform.position + transform.forward * 250);

                GameObject bullet = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation) as GameObject;
                bullet.GetComponent<BulletScript>().bulletSpeed = speedOfBullets;
                bullet.GetComponent<BulletScript>().bulletDamage = this.bulletDamage;
            }
        }
    }
}
