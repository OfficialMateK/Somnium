using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScriptJohan : MonoBehaviour
{
    private Animator animator;

    public GameObject bulletPrefab;
    public int bulletDamage;
    public float speedOfBullets;
    public Transform weaponPoint;
    public ParticleSystem muzzleFlash;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            muzzleFlash.Play();
            animator.SetTrigger("Shoot");
            //animator.SetTrigger("Reload");

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                weaponPoint.LookAt(hit.point);

                GameObject bullet = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation) as GameObject;
                bullet.GetComponent<BulletScriptJohan>().bulletSpeed = speedOfBullets;
                bullet.GetComponent<BulletScriptJohan>().bulletDamage = this.bulletDamage;
            }
            else
            {
                weaponPoint.LookAt(transform.position + transform.forward * 250);
                GameObject bullet = Instantiate(bulletPrefab, weaponPoint.position, weaponPoint.rotation) as GameObject;
                bullet.GetComponent<BulletScriptJohan>().bulletSpeed = speedOfBullets;
                bullet.GetComponent<BulletScriptJohan>().bulletDamage = this.bulletDamage;
            }
        }
    }
}
