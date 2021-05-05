using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customPhysics3D : MonoBehaviour
{
	//Somnium
	[SerializeField] private bool toggleGravity;
	
	/*[SerializeField] private float impactArea;
	[SerializeField] private float dragCoeffiecient;
	[SerializeField] private float atmosphericDensity;
	
	[SerializeField] private float terminalVelocity; // m/s*/
	[SerializeField] private float gravityConstant = 9.82f; // m/s^2
	[SerializeField] private float mass; // Kg
	[SerializeField] private float collisionMaxRange; // m
	[SerializeField] private float collisionMargin; // m
	//[SerializeField] private float capsulePointDistance;
	//[SerializeField] private float timeDelay = 1; // s
	[SerializeField] private CapsuleCollider capsule;
	[SerializeField] private LayerMask collisionMask;
	
	[SerializeField] private Vector3 velocity;
	
	private float capsuleRadius; // m[SerializeField] 
	private float capsuleHeight; // m[SerializeField] 
	
	private bool airbourne;
	
	//[SerializeField] private Vector3 pointTwo;
	
	void Awake()
	{
		capsule = GetComponent<CapsuleCollider>();
		capsuleRadius = capsule.radius;
		capsuleHeight = capsule.height;
		
	}

    void FixedUpdate()
    {
		//YVelocityUpdate();
		SumForce();
		//GetComponent<collisionScript3D>().Collisions();
		//GetComponent<playerMovement3D>().Movement();
    }
	
	/*private void GravityForce()
	{
		if(toggleGravity == true)
		{
			Vector3 gravityVector = new Vector3(0,Vector3.down.y * gravityConstant * Time.deltaTime,0);
			
			transform.position += gravityVector;
		}
		else
		{
			return;
		}
	}
	
	public float GetGravity()
	{
		return gravityConstant;
	}*/
	
	public bool GetAirbourne()
	{
		return airbourne;
	}
	
	private void SumForce()
	{
		if(transform.tag == "Player")
		{
			transform.position += PlayerMovement();
		}
		if(transform.tag == "Enemy")
		{
		}
		
	}
	
	private Vector3 PlayerMovement()
	{
		if(toggleGravity)
		{
			Vector3 input = GetComponent<customPlayerController3D>().GetInputMove();
			velocity = input + GravityForce() /*+ NormalForceVertical(input + GravityForce())*/ + NormalForceHorizontal(input + GravityForce()) + NormalForceVertical(input + GravityForce())/**/ /*+ NormalForceGeneral(input + GravityForce())*/;
		
			Vector3 output = velocity;
			return output;
		}
		else
		{
			Vector3 input = GetComponent<customPlayerController3D>().GetInputMove();
			velocity = input + NormalForceGeneral(input) + NormalForceHorizontal(input);
			
		
			Vector3 output = velocity;
			return output;
		}
	}
	
	/**/private Vector3 GravityForce()
	{
		Vector3 gravityVector = new Vector3(0,Vector3.down.y * mass * gravityConstant * Time.deltaTime,0);
		
		//transform.position += gravityVector;
		
		Vector3 output = gravityVector;
		return output;
	}
	
	//Start
	
	private Vector3 NormalForceVertical(Vector3 movement)
	{
		RaycastHit hit;
		RaycastHit rayHit;
		Vector3 currentNormal = Vector3.zero;
		Vector3 projection = Vector3.zero;
		Vector3 output = Vector3.zero;
				
		//Vector3 origin = transform.position;
		Vector3 pointOne = transform.position + Vector3.up * (capsuleHeight / 2 - capsuleRadius);
		Vector3 pointTwo = transform.position + Vector3.down * (capsuleHeight / 2 - capsuleRadius);
		Vector3 direction = /*Vector3.down;*/new Vector3(0, movement.y, 0);
		
		Debug.DrawRay(transform.position, direction * capsuleRadius, Color.red);

		if(Physics.CapsuleCast(pointOne, pointTwo, capsuleRadius + collisionMargin, direction, out hit, collisionMaxRange , collisionMask))
		{
			/*if(((transform.position.y - capsuleHeight/2) + collisionMargin) - hit.point.y <= collisionMargin)
			{
				velocity = new Vector3(velocity.x, 0, velocity.z);
			}*/
			
			/*if(hit.distance < collisionMargin)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + collisionMargin, transform.position.z);
			}*/
			
			if(Vector3.Dot(direction, currentNormal) > 0)
			{
				projection = Vector3.zero;
			}
			else
			{
				currentNormal = hit.normal;
				print(currentNormal);
				projection = Vector3.Dot(direction, currentNormal) * currentNormal;
				output = -projection;
			}
		}
		CheckGround(currentNormal);
		//print(hit.distance);
		return output;
	}
	
	private Vector3 NormalForceHorizontal(Vector3 movement)
	{
		RaycastHit hit;
		Vector3 currentNormal = Vector3.zero;
		Vector3 projection = Vector3.zero;
		Vector3 output = Vector3.zero;
				
		//Vector3 origin = transform.position;
		Vector3 pointOne = transform.position + Vector3.up * (capsuleHeight / 2 - capsuleRadius);
		Vector3 pointTwo = transform.position + Vector3.down * (capsuleHeight / 2 - capsuleRadius);
		Vector3 directionHorizontal = new Vector3(movement.x, 0, movement.z);
		
		Debug.DrawRay(transform.position, directionHorizontal * 10, Color.red);

		if(Physics.CapsuleCast(pointOne, pointTwo, capsuleRadius + collisionMargin, directionHorizontal, out hit, collisionMaxRange , collisionMask))
		{
			//currentNormal = hit.normal;
			//projection = Vector3.Dot(movement, currentNormal) * currentNormal;
			//output = -projection;
			
			if(hit.distance <= collisionMargin)
			{
				if(hit.normal.x > 0)
				{
					transform.position += new Vector3(collisionMargin, 0, 0);
				}
				if(hit.normal.y > 0)
				{
					transform.position += new Vector3(0, collisionMargin, 0);
				}
				if(hit.normal.z > 0)
				{
					transform.position += new Vector3(0, 0, collisionMargin);
				}
				if(hit.normal.x < 0)
				{
					transform.position -= new Vector3(collisionMargin, 0, 0);
				}
				if(hit.normal.y < 0)
				{
					transform.position -= new Vector3(0, collisionMargin, 0);
				}
				if(hit.normal.z < 0)
				{
					transform.position -= new Vector3(0, 0, collisionMargin);
				}
				print("is collisionMargin");
			}
			
			if(Vector3.Dot(directionHorizontal, currentNormal) > 0)
			{
				projection = Vector3.zero;
			}
			else
			{
				currentNormal = hit.normal;
				projection = Vector3.Dot(directionHorizontal, currentNormal) * currentNormal;
				output = -projection;
			}
		}
		return output;
	}
	
	private Vector3 NormalForceGeneral(Vector3 movement)
	{
		RaycastHit hit;
		Vector3 currentNormal = Vector3.zero;
		Vector3 projection = Vector3.zero;
		Vector3 output = Vector3.zero;
				
		//Vector3 origin = transform.position;
		Vector3 pointOne = transform.position + Vector3.up * (capsuleHeight / 2 - capsuleRadius);
		Vector3 pointTwo = transform.position + Vector3.down * (capsuleHeight / 2 - capsuleRadius);
		
		//Debug.DrawRay(pointTwo, movement * 10, Color.red);
		//Debug.DrawRay(pointOne, Vector3.up * capsuleRadius, Color.red);
		//Debug.DrawRay(pointTwo, Vector3.back * capsuleRadius, Color.red);
		//Debug.DrawRay(pointTwo, Vector3.right * collisionMaxRange, Color.blue);
		//Debug.DrawRay(pointTwo, Vector3.left * collisionMargin, Color.yellow);
		//Debug.DrawRay(pointTwo, Vector3.down * (capsuleRadius + collisionMargin), Color.green);

		if(Physics.CapsuleCast(pointOne, pointTwo, capsuleRadius + collisionMargin, movement, out hit, collisionMaxRange , collisionMask))
		{
			//currentNormal = hit.normal;
			//projection = Vector3.Dot(movement, currentNormal) * currentNormal;
			//output = -projection;
			
			if(hit.distance <= collisionMargin)
			{
				if(hit.normal.x > 0)
				{
					transform.position += new Vector3(collisionMargin, 0, 0);
				}
				if(hit.normal.y > 0)
				{
					transform.position += new Vector3(0, collisionMargin, 0);
				}
				if(hit.normal.z > 0)
				{
					transform.position += new Vector3(0, 0, collisionMargin);
				}
				if(hit.normal.x < 0)
				{
					transform.position -= new Vector3(collisionMargin, 0, 0);
				}
				if(hit.normal.y < 0)
				{
					transform.position -= new Vector3(0, collisionMargin, 0);
				}
				if(hit.normal.z < 0)
				{
					transform.position -= new Vector3(0, 0, collisionMargin);
				}
				print("is collisionMargin");
				/*if(transform.position.y < hit.point.y)
				{
					transform.position = new Vector3
					(
					transform.position.x,
					hit.point.y + collisionMargin,
					transform.position.z
					);
				}
				if(transform.position.x < hit.point.x)
				{
					transform.position = new Vector3
					(
					hit.point.x + collisionMargin,
					transform.position.y,
					transform.position.z
					);
				}
				if(transform.position.z < hit.point.z)
				{
					transform.position = new Vector3
					(
					transform.position.x,
					transform.position.y,
					hit.point.z + collisionMargin
					);
				}*/
			}
			
			if(Vector3.Dot(movement, currentNormal) > 0)
			{
				projection = Vector3.zero;
			}
			else
			{
				currentNormal = hit.normal;
				projection = Vector3.Dot(movement, currentNormal) * currentNormal;
				
				output = -projection;
			}
		}
		print(hit.distance);
		return output;
	}

	private void CheckGround(Vector3 normal)
	{
		if(normal == Vector3.up)
		{
			airbourne = false;
		}
		else
		{
			airbourne = true;
		}
	}
}

