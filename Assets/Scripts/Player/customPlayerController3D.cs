
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customPlayerController3D : MonoBehaviour
{
	
	//Somnium
	[SerializeField] private float speed;
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float mouseSensitivity;
	[SerializeField] private float deathHeight;
	[SerializeField] private float backingSpeedFactor;
	[SerializeField] private float strafingSpeedFactor;

    void Awake()
    {
	}
    void FixedUpdate()
    {
		DeathFall();
    }
	
	
	public float GetMouseSensitivity()
	{
		float output = mouseSensitivity;
		return output;
	}
	
	public Vector3 GetInputMove()
	{
		bool grounded = !GetComponent<customPhysics3D>().GetAirbourne();

		float xDirection = Input.GetAxisRaw("Horizontal");
		float zDirection = Input.GetAxisRaw("Vertical");
		bool yInput = false;
		
		if(grounded)
		{
			yInput = Input.GetButton/*Down*/("Jump");
		}
		else
		{
			yInput = false;
		}
		
		Vector3 velocity = Jump(yInput) + XMovement(xDirection) + ZMovement(zDirection);
		Vector3 inputMovement = transform.rotation * velocity;
		
		
		Vector3 output = inputMovement/*.normalized*/;
		return output;
	}
	
	private Vector3 Jump(bool input)
	{
		Vector3 jumpVector = Vector3.zero;
		
		if(input)
		{
			jumpVector = new Vector3(0,jumpSpeed,0);
		}
		else
		{}
		
		Vector3 output = jumpVector;
		return output;
	}
	
	private Vector3 XMovement(float direction)
	{
		Vector3 xVector = new Vector3(speed,0,0);
		xVector = xVector * direction;
		
		if(xVector.x != 0)
		{
			xVector = xVector * strafingSpeedFactor;
		}
		
		Vector3 output = xVector;
		return output;
	}
	
	private Vector3 ZMovement(float direction)
	{
		Vector3 zVector = new Vector3(0,0,speed);
		zVector = zVector * direction;
		
		if(zVector.z < 0)
		{
			zVector = zVector * backingSpeedFactor;
		}
		
		Vector3 output = zVector;
		return output;
	}
	
	private void DeathFall()
	{
		if(transform.position.y <= deathHeight)
		{
			//Destroy(gameObject);
			GetComponent<PlayerHealth>().SetHealth(0);
			print("ded?");
		}
	}
}
