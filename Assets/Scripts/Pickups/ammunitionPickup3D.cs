using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammunitionPickup3D : MonoBehaviour
{
	[SerializeField] GameObject player; 
	
    //[SerializeField] private int ammunition;
	[SerializeField] private int maxAmmunition;
    [SerializeField] private int minAmmunition;
	
	//[SerializeField] float MinHealth;
	//[SerializeField] float MaxHealth;
	//[SerializeField] float decay = 20;
	[SerializeField] float killBoundry = -100;

	private string newline = "\n";
	
	//int maxAmmunitionCount;
	
	// Start is called before the first frame update
	void Start()
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
		player = GameObject.FindGameObjectWithTag("Player");
		//maxAmmunitionCount = player.GetComponent<playerStats_IU1>().maxPlayerHealth;
	}

	/*void Variables()
	{
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
	}*/

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			print("Pickup collided with player");
			int ammunition = /*collision.gameObject*/player.GetComponent<customWeaponController3D>().GetAmmunitionCount();
			int ammunitionCapacity = /*collision.gameObject*/player.GetComponent<customWeaponController3D>().GetMagazineAmmunitionCapacity();
			int randomAmmunition = AmmunitionRandom(minAmmunition, maxAmmunition);
			print("ammo : " + ammunition + newline + "capacity : " + ammunitionCapacity + newline + "randomNr : " + randomAmmunition);

			if (ammunition < ammunitionCapacity && ammunition + randomAmmunition >= ammunitionCapacity)
			{
				collision.gameObject.GetComponent<customWeaponController3D>().SetAmmunitionCount(ammunitionCapacity);
				Remove();
				/*print("maxPlayerHP = "+maxPlayerHP);
				print("pHP = "+pHP);
				print("pHP + randomHPvalue = "+(pHP + randomHPvalue));*/
			}
			if (ammunition < ammunitionCapacity && ammunition + randomAmmunition < ammunitionCapacity)
			{
				//print("start self-destruction");
				collision.gameObject.GetComponent<customWeaponController3D>().AddAmmunitionCount(randomAmmunition);
				Remove();
			}
			if (ammunition >= ammunitionCapacity && ammunition + randomAmmunition >= ammunitionCapacity)
			{
				return;
			}

		}
	}

	private int AmmunitionRandom(int min, int max)
	{
		int overheadValue = (max + 1);

		int ammunitionAdded = Random.Range(min, overheadValue);
		//print(HP_Added+" HP will be added");
		return (ammunitionAdded);
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
