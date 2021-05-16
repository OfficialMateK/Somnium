using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class customWeaponController3D : MonoBehaviour
{
	[SerializeField] private string weaponName; //The name of your weapon
	
	[SerializeField] private float speed; //Speed of the projectile in m/s	
	[SerializeField] private float zoomFactor; //The factor by which your view will be zoomed in by;

	[SerializeField] private int rateOfFire; //The cyclic rate of fire, in rounds per minute, that is fired
	[SerializeField] private int damage; //Ammount of health each hit removes from the hit target
	[SerializeField] private int ammunitionCount; //Ammount of ammunition currently stored in the magazine
	[SerializeField] private int magazineCount; //Ammount of magazines currently stored
	[SerializeField] private int magazineCountCapacity; //Ammount of maagazines that can be stored for said weapon
	[SerializeField] private int magazineAmmunitionCapacity; //Ammount of ammunition that can be stored in each magazine
	[SerializeField] private int storedAmmunition; //Total ammount of ammunition that is stored at any given time
	[SerializeField] private int burstLength; //Ammount of projectiles fired each burst during burst fire mode
	[SerializeField] private int currentMode = 1; //Index for active fire mode, default : single fire
	[SerializeField] private int reloadTime; //The time it takes to reload a magazine in seconds
	//[SerializeField] private int firemodes;
	
	[SerializeField] private enum weaponType {Melee, Pistol, Rifle, Launcher};
	
	[SerializeField] private weaponType typeOfWeapon;
	//[SerializeField] private fireMode modeOfFire;
	
	[SerializeField] private bool hasAuto; //Allows for auto fire mode
	[SerializeField] private bool hasBurst; //Allows for burst fire mode
	[SerializeField] private bool hasZoom; //Allows for zoomed view

	[SerializeField] private GameObject projectilePrefab; //The procectile that the weapon fires
	[SerializeField] private GameObject ammunitionCounterUI; //Displays ammunition count in active magazine on the
	[SerializeField] private GameObject storedAmmunitionCounterUI; //Displays ammunition count in active magazine on the UI
	[SerializeField] private GameObject fireModeCounterUI; //Displays ammunition count in active magazine on the UI
	[SerializeField] private GameObject aimingCrosshair; //Crosshair for targeting
	[SerializeField] private GameObject reloadCrosshair; //Crosshair for displaying when you are reloading
	[SerializeField] private GameObject fireDenialCrosshair; //Crosshair for displaying when you can and can't shoot
	[SerializeField] private GameObject outOfAmmoIndicator; //Indicates when you are out of stored ammo
	[SerializeField] private Color hostileTargetingColour; //Colour of the crosshair when targeting enemy
	[SerializeField] private Color neutralTargetingColour; //Colour of the crosshair when targeting nothing

	[SerializeField] private Transform firePoint; //The procectile that the weapon fires

	[SerializeField] private ParticleSystem muzzleFlash; //The flash that appears att the end of the rifle barrel from when the gunpowder is ignited in the projectile (assuming it has a gunpowder propellant)

	[SerializeField] private List <string> fireMode =  new List <string> {"safe", "single"}; //Default fire modes
	
	private bool allowFire = true;
	private bool allowControl = true;
	private bool reloaded = true;
	private bool reloadInProgress;
	private bool zoomed;
	//private bool allowBurst = true;
	private bool allowModeToggle = true;

	private float defaultView;

	private GameObject playerCamera;
	private GameObject player;

	
	
    // Start is called before the first frame update
    void Start()
    {
		VariablesAndConstants();
        AddFireModes();
		CheckReloadAtStart();
		StoredAmmunition();
    }

    // Update is called once per cycle
    void FixedUpdate()
    {
        Fire();
		MagazineController();
		Reload();
		TypeController();
		AmmunitionController();
		GUICounter(ammunitionCounterUI, ammunitionCount);
		GUICounter(storedAmmunitionCounterUI, storedAmmunition);
		GUIMessage(fireModeCounterUI, fireMode[currentMode]);
		WeaponZoom(zoomFactor);
		ChangeCrosshairShape();
		ChangeCrosshairColour();
		AmmunitionCapacityRestriction();
		//Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z), playerCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
		//TempExit();
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

	public int GetMagazineAmmunitionCapacity()
	{
		return magazineAmmunitionCapacity;
	}

	public int GetMagazineCount()
	{
		return magazineCount;
	}

	public int GetMagazineCountCapacity()
	{
		return magazineCountCapacity;
	}

	public int GetStoredAmmunition()
	{
		return storedAmmunition;
	}

	public void SetStoredAmmunition(int value)
	{
		storedAmmunition = value;
	}

	public void AddStoredAmmunition(int value)
	{
		storedAmmunition += value;
	}

	public void SetMagazineCount(int value)
	{
		magazineCount = value;
	}

	public void AddMagazineCount(int value)
	{
		magazineCount += value;
	}

	public void SetAmmunitionCount(int value) 
	{
		ammunitionCount = value;
	}

	public void AddAmmunitionCount(int value)
	{
		ammunitionCount += value;
	}

	private void VariablesAndConstants()
	{
		ammunitionCount = magazineAmmunitionCapacity;
		magazineCount = magazineCountCapacity;
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
		player = GameObject.FindGameObjectWithTag("Player");
		defaultView = playerCamera.GetComponent<Camera>().fieldOfView;
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
		RaycastHit hit;
		Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);

		//Debug.DrawRay(, playerCamera.transform.TransformDirection(Vector3.forward) * 100, Color.red);
		Debug.DrawLine(playerCamera.transform.position, hit.point, Color.cyan);
		Debug.DrawLine(firePoint.transform.position, hit.point, Color.red);
		//print(hit.collider.tag/* + " " + Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)*/);
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
		//print("pew");
		RaycastHit hit;
		ammunitionCount -= 1;

		muzzleFlash.Play();

		if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
		{
			firePoint.LookAt(hit.point);

			GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
			projectile.GetComponent<BulletScript>().bulletSpeed = speed;
			projectile.GetComponent<BulletScript>().bulletDamage = damage;
		}
		else
		{
			firePoint.LookAt(playerCamera.transform.position + playerCamera.transform.forward * 250);

			GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
			projectile.GetComponent<BulletScript>().bulletSpeed = speed;
			projectile.GetComponent<BulletScript>().bulletDamage = damage;
		}
		if(ammunitionCount <= 0)
		{
			ammunitionCount = 0;
			allowFire = false;
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
		if(Input.GetButtonDown("Toggle Fire Mode") && allowModeToggle)
		{
			//print("?");
			allowModeToggle = false;
			currentMode ++;
			FireModeController();
		}
		allowModeToggle = true;
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
		reloadInProgress = true;
        allowFire = false;
        reloaded = false;
        yield return new WaitForSeconds(reloadTime);
        if (!reloaded)
        {
            if (magazineCount > 0 && ammunitionCount < magazineAmmunitionCapacity && storedAmmunition >= magazineAmmunitionCapacity)
            {
				print("r1");
                //reloaded = true;
				if (ammunitionCount <= 0)
				{
					magazineCount -= 1;
				}
				//allow_OOA_Sound = false;
				storedAmmunition -= magazineAmmunitionCapacity;
				storedAmmunition += ammunitionCount;
				ammunitionCount = magazineAmmunitionCapacity;
            }
			if (magazineCount > 0 && ammunitionCount <= 0 && storedAmmunition >= magazineAmmunitionCapacity)
			{
				print("r2");
				//reloaded = true;
				magazineCount -= 1;
				//allow_OOA_Sound = false;
				storedAmmunition -= magazineAmmunitionCapacity;
				storedAmmunition += ammunitionCount;
				ammunitionCount = magazineAmmunitionCapacity;
			}
			if(magazineCount > 0 && ammunitionCount < magazineAmmunitionCapacity && storedAmmunition > 0)
            {
				print("r3");
				magazineCount = 0;
                ammunitionCount = 0;
                ammunitionCount = storedAmmunition;
				storedAmmunition = 0;
                
            }
			else if(magazineCount <= 0 && ammunitionCount < magazineAmmunitionCapacity && storedAmmunition <= 0)
            {
				print("r4");
				magazineCount = 0;
                ammunitionCount = 0;
				storedAmmunition = 0;
            }
        }
		if (ammunitionCount > 0)
		{
			print("reloaded");
			reloaded = true;
		}
		if (ammunitionCount <= 0 && storedAmmunition <= 0)
		{
			reloaded = false;
			print("out of ammo");
		}

		if (reloaded) 
		{
			allowFire = true;
		}
		reloadInProgress = false;
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

	private void AmmunitionController() 
	{
		if(ammunitionCount >= magazineAmmunitionCapacity) 
		{
			ammunitionCount = magazineAmmunitionCapacity;
		}
		if(ammunitionCount <= 0) 
		{
			ammunitionCount = 0;
		}
		if (magazineCount >= magazineCountCapacity) 
		{
			magazineCount = magazineCountCapacity;
		}
	}

	private void GUICounter(GameObject target, int value) 
	{
		target.GetComponent<Text>().text = "" + value;
	}

	private void GUIMessage(GameObject target, string message)
	{
		target.GetComponent<Text>().text = message;
	}

	private void StoredAmmunition() 
	{
		storedAmmunition = magazineCount * magazineAmmunitionCapacity;
	}

	private void WeaponZoom(float zoomFactor) 
	{
		float zoomedView = defaultView/zoomFactor;

		if (hasZoom) 
		{
			if (Input.GetButtonDown("Zoom") && zoomed)
			{
				zoomed = false;
			}
			else if (Input.GetButtonDown("Zoom") && !zoomed)
			{
				zoomed = true;
			}

			if (zoomed)
			{
				playerCamera.GetComponent<Camera>().fieldOfView = zoomedView;
			}
			else
			{
				playerCamera.GetComponent<Camera>().fieldOfView = defaultView;
			}
		}
	}

	private void ChangeCrosshairShape() 
	{
		if (ammunitionCount > 0)
		{
			//print("s1");
			aimingCrosshair.SetActive(true);
			fireDenialCrosshair.SetActive(false);
		}
		
		if (reloaded)
		{
			//print("s2");
			aimingCrosshair.SetActive(true);
			reloadCrosshair.SetActive(false);
		}

		if (ammunitionCount <= 0)
		{
			//print("s3");
			aimingCrosshair.SetActive(false);
			fireDenialCrosshair.SetActive(true);
			reloadCrosshair.SetActive(false);
		}

		if (!reloaded && storedAmmunition > 0 && reloadInProgress) 
		{
			//print("s4");
			aimingCrosshair.SetActive(false);
			fireDenialCrosshair.SetActive(false);
			reloadCrosshair.SetActive(true);
		}

		if (storedAmmunition > 0)
		{
			//print("s5");
			outOfAmmoIndicator.SetActive(false);
		}

		if (storedAmmunition <= 0 && ammunitionCount <= 0)
		{
			//print("s6");
			outOfAmmoIndicator.SetActive(true);
		}

	}

	private void ChangeCrosshairColour() 
	{
		RaycastHit hit;

		if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
		{
			if (hit.collider.CompareTag("New Melee Enemy") || hit.collider.CompareTag("Melee Enemy") || hit.collider.CompareTag("Distance Enemy"))
			{
				aimingCrosshair.GetComponent<Image>().color = hostileTargetingColour;
			}
			else 
			{
				aimingCrosshair.GetComponent<Image>().color = neutralTargetingColour;
			}
		}

		
	}

	private void MagazineController() 
	{
		if (storedAmmunition > 0)
		{
			int replenishedCount = (int)Mathf.Ceil(storedAmmunition / magazineAmmunitionCapacity);

			magazineCount = replenishedCount;
		}

		if (ammunitionCount > 0 || storedAmmunition > 0 && storedAmmunition <= magazineAmmunitionCapacity) 
		{
			magazineCount = 1;
		}
	}

	private void AmmunitionCapacityRestriction() 
	{
		int storedAmmunitionCapacity = magazineCountCapacity * magazineAmmunitionCapacity;

		if (storedAmmunition >= storedAmmunitionCapacity) 
		{
			storedAmmunition = storedAmmunitionCapacity;
		}

		if (ammunitionCount >= magazineAmmunitionCapacity) 
		{
			ammunitionCount = magazineAmmunitionCapacity;
		}
	}
}