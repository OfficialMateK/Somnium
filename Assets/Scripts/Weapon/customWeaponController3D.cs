using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class customWeaponController3D : MonoBehaviour
{
	[SerializeField] private string weaponName; //The name of your weapon
	
	[SerializeField] private float speed; //Speed of the projectile in m/s
	
	[SerializeField] private int rateOfFire; //The cyclic rate of fire, in rounds per minute, that is fired
	[SerializeField] private int damage; //Ammount of health each hit removes from the hit target
	[SerializeField] private int ammunitionCount; //Ammount of ammunition currently stored in the magazine
	[SerializeField] private int magazineCount; //Ammount of magazines currently stored
	[SerializeField] private int magazineCountCapacity; //Ammount of maagazines that can be stored for said weapon
	[SerializeField] private int magazineAmmunitionCapacity; //Ammount of ammunition that can be stored in each magazine
	[SerializeField] private int burstLength; //Ammount of projectiles fired each burst during burst fire mode
	[SerializeField] private int currentMode = 1; //Index for active fire mode, default : single fire
	[SerializeField] private int reloadTime; //The time it takes to reload a magazine in seconds
	//[SerializeField] private int firemodes;
	
	[SerializeField] private enum weaponType {Melee, Pistol, Rifle, Launcher};
	
	[SerializeField] private weaponType typeOfWeapon;
	//[SerializeField] private fireMode modeOfFire;
	
	[SerializeField] private bool hasAuto; //Allows for auto fire mode
	[SerializeField] private bool hasBurst; //Allows for burst fire mode
	
	[SerializeField] private GameObject projectilePrefab; //The procectile that the weapon fires
	
	[SerializeField] private Transform firePoint; //The procectile that the weapon fires
	
	private bool allowFire = true;
	private bool allowControl = true;
	private bool reloaded;
	private bool allowBurst = true;
	
	private GameObject camera;
	
	[SerializeField] private List <string> fireMode =  new List <string> {"safe", "single"}; //Default fire modes
	
    // Start is called before the first frame update
    void Start()
    {
		Variables();
        AddFireModes();
		CheckReloadAtStart();
    }

    // Update is called once per cycle
    void FixedUpdate()
    {
        Fire();
		Reload();
		TypeController();
		TempExit();
		//print(fireMode[currentMode]);
    }
	
	public string GetFireMode(int index)
	{
		return fireMode[index];
	}
	
	public string GetActiveFireMode()
	{
		return GetFireMode(currentMode);
	}
	
	public int GetAmmunitionCount()
	{
		return ammunitionCount;
	}
	
	public int GetMagazineCount()
	{
		return ammunitionCount;
	}
	
	private void Variables()
	{
		ammunitionCount = magazineAmmunitionCapacity;
		magazineCount = magazineCountCapacity;
		camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	private void TypeController()
	{
		if(typeOfWeapon == weaponType.Melee)
		{
			MeleeWeapon();
		}
		else//if(typeOfWeapon != weaponType.Melee)
		{
			RangedWeapon();
		}
	}
	
	private void MeleeWeapon()
	{
		if(allowControl)
		{
			allowControl = false;
			
			fireMode.Clear();
			fireMode.Add("safe"); //Just so that Unity will compile for now
			fireMode.Add("melee");
		
			hasAuto = false;
			hasBurst = false;
		}
	}
	
	private void RangedWeapon()
	{
		ToggleFireMode();
	}
	
	private void Fire()
	{
		if(fireMode[currentMode] == "safe")
		{
			Safety();
		}
		if(fireMode[currentMode] == "single")
		{
			SingleFire();
		}
		if(hasBurst && fireMode[currentMode] == "burst")
		{
			BurstFire();
		}
		if(hasAuto && fireMode[currentMode] == "auto")
		{
			AutoFire();
		}
		if(fireMode[currentMode] == "melee")
		{
			MeleeAttack();
		}
	}
	
	private void ShootProjectile()
	{
		ammunitionCount -= 1;
		
		RaycastHit hit;

		
		if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
		{
            firePoint.LookAt(hit.point);

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.GetComponent<BulletScript>().bulletSpeed = speed;
            projectile.GetComponent<BulletScript>().bulletDamage = damage;
        } 
		else
		{
            firePoint.LookAt(camera.transform.position + camera.transform.forward * 250);

    		GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.GetComponent<BulletScript>().bulletSpeed = speed;
            projectile.GetComponent<BulletScript>().bulletDamage = damage;
        }
		if(ammunitionCount <= 0)
		{
			ammunitionCount = 0;
		}
	}
	
	private void Safety()
	{
		allowFire = false;
	}
	
	private void SingleFire()
	{
		allowFire = true;
		
		if(allowFire && Input.GetButtonDown("Fire1") && ammunitionCount > 0 && reloaded)
		{
			//ammunitionCount -= 1;
			
			//GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
			//ShootProjectile();
			
			StartCoroutine("FireInterval");
		}
	}
	
	private void BurstFire()
	{
		//allowFire = true;
		//allowBurst = true;
		//int burstCount = 0;
		
		if(allowFire && Input.GetButtonDown("Fire1") && ammunitionCount > 0 && reloaded)/* && allowBurst && burstCount < burstLength*/
		{
			//allowBurst = false;
			//ammunitionCount -= 1;
			
			for(int burstCount = 0; burstCount < burstLength; burstCount ++)
			{
				//ShootProjectile();
				
				StartCoroutine("FireInterval");
			}
			//allowBurst = true;
			//GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
		}
	}
	
	private void AutoFire()
	{
		//allowFire = true;
		
		if(allowFire && Input.GetButton("Fire1") && ammunitionCount > 0 && reloaded)
		{
			//ammunitionCount -= 1;
			
			//GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
			//ShootProjectile();
			
			StartCoroutine("FireInterval");
		}
	}
	
	private void MeleeAttack()
	{
		allowFire = true;
		
		print("knif");
	}
	
	IEnumerator FireInterval()
	{
		ShootProjectile();
		float interval = 60/rateOfFire;
		allowFire = false;
		yield return new WaitForSeconds(interval);
		allowFire = true;
	}
	
	private void SetFireActive(bool toggle)
	{
		allowFire = toggle;
	}
	
	private void FireModeController()
	{
		if(!hasBurst && currentMode >= fireMode.Count)
		{
			currentMode = 0;
		}
		if(!hasAuto && currentMode >= fireMode.Count)
		{
			currentMode = 0;
		}
		if(currentMode >= fireMode.Count)
		{
			currentMode = 0;
		}
	}
	
	private void ToggleFireMode()
	{
		if(Input.GetButtonDown("Toggle Fire Mode")/* && allowModeToggle*/)
		{
			//print("?");
			//allowModeToggle = false;
			currentMode ++;
			FireModeController();
		}
		//allowModeToggle = true;
	}
	
	private void AddFireModes()
	{
		if(hasBurst)
		{
			fireMode.Add("burst");
		}
		if(hasAuto)
		{
			fireMode.Add("auto");
		}
	}
	
	private void Reload()
    {
        if (Input.GetButtonDown("Reload"))
        {
			print("reloading");
            StartCoroutine("ReloadTimer");
        }
		
    }

    IEnumerator ReloadTimer()
    {
        allowFire = false;
        reloaded = false;
        yield return new WaitForSeconds(reloadTime);
        if (reloaded == false)
        {
            if (magazineCount > 0 && ammunitionCount < magazineAmmunitionCapacity)
            {
                reloaded = true;
                magazineCount -= 1;
				//allow_OOA_Sound = false;
                ammunitionCount = magazineAmmunitionCapacity;
            }
            if (magazineCount <= 0 && ammunitionCount < magazineAmmunitionCapacity)
            {
                magazineCount = 0;
                ammunitionCount = 0;
                /*if (weaponSelected)
                {
                    weaponSelected = false;
                }
                else
                {
                    yield return new WaitForSeconds(0);
                }*/
            }
        }
        allowFire = true;
    }
	
	private void CheckReloadAtStart()
	{
		if(ammunitionCount <= 0)
		{
			reloaded = false;
		}
		else
		{
			reloaded = true;
		}
	}
	
	private void TempExit()
	{
		if (Input.GetKey("escape"))
        {
			print("yeet!");
            Quit();
        }
	}
	
	private void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}