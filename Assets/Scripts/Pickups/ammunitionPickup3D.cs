using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammunitionPickup3D : MonoBehaviour
{
	[SerializeField] private int maximumAmmunitionCount;
    [SerializeField] private int minimumAmmunitionCount;
	
	//[SerializeField] float decay = 20;
	[SerializeField] float killBoundry = -100;

	private string newline = "\n";
	
	private GameObject weapon;
	
	// Start is called before the first frame update
	void Awake()
	{
		Constants();
		//Decay();
	}

	void FixedUpdate()
	{
		KillZone();
	}

	private void Constants()
	{
		weapon = GameObject.FindGameObjectWithTag("Weapon");
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			print("Pickup collided with player"); //weapon.GetComponent<customWeaponController3D>().;

			int ammunition = weapon.GetComponent<customWeaponController3D>().GetAmmunitionCount();
			int ammunitionCapacity = weapon.GetComponent<customWeaponController3D>().GetMagazineAmmunitionCapacity();
			int capacityForMagazines = weapon.GetComponent<customWeaponController3D>().GetMagazineCountCapacity();
			int storedAmmunition = weapon.GetComponent<customWeaponController3D>().GetStoredAmmunition();
			int storedAmmunitionCapacity = ammunitionCapacity * capacityForMagazines;
			int recievedAmmunition = AmmunitionRecieved(minimumAmmunitionCount, maximumAmmunitionCount);

			print("stored&recievedAmmo: " + (storedAmmunition + recievedAmmunition) + newline + "recievedAmmo: " + recievedAmmunition);

			/*if (ammunition < ammunitionCapacity && (storedAmmunition + recievedAmmunition) >= storedAmmunitionCapacity)
			{
				//weapon.GetComponent<customWeaponController3D>().SetAmmunitionCount(ammunitionCapacity);
				weapon.GetComponent<customWeaponController3D>().SetStoredAmmunition(storedAmmunitionCapacity);
				Remove();
				print("5");
			}*/
			if (storedAmmunition < storedAmmunitionCapacity)
			{
				//weapon.GetComponent<customWeaponController3D>().SetAmmunitionCount(ammunitionCapacity);
				//if (storedAmmunition + recievedAmmunition < storedAmmunitionCapacity) 
				//{
					weapon.GetComponent<customWeaponController3D>().AddStoredAmmunition(recievedAmmunition);
				/*}
				if (storedAmmunition + recievedAmmunition >= storedAmmunitionCapacity)
				{
					weapon.GetComponent<customWeaponController3D>().SetStoredAmmunition(storedAmmunitionCapacity);
				}*/

				Remove();
				//print("5");
			}
			/*if (ammunition >= ammunitionCapacity && (storedAmmunition + recievedAmmunition) >= storedAmmunitionCapacity)
			{
				print("7");
				return;
			}*/
		}
	}

	private int AmmunitionRecieved(int minimumAmmunition, int maximumAmmunition)
	{
		int overheadValue = (maximumAmmunition + 1);

		int magazinesAdded = Random.Range(minimumAmmunition, overheadValue);
		//print(HP_Added+" HP will be added");
		return (magazinesAdded);
	}
	private void Remove()
	{
		print("Pickup was then destroyed.");
		Destroy(gameObject);
	}
	/*private void Decay()
	{
		//print("Pickup decay started.");
		Destroy(gameObject, decay);
	}*/
	private void KillZone()
	{
		float zPosition = gameObject.transform.position.z;
		if (zPosition <= killBoundry)
		{
			Remove();
		}
	}
}
