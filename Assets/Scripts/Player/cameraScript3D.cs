using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript3D : MonoBehaviour
{
	//Somnium
	
	[SerializeField] private float minimumRotationX;
	[SerializeField] private float maximumRotationX;
	[SerializeField] private float ray;
	
	[SerializeField] private Vector3 cameraOffset;
	
	[SerializeField] private LayerMask cameraCollisionMask;
	
	//private Transform cameraTransform;
	private float mouseSensitivity;//[SerializeField]
	private float rotationX;//[SerializeField] 
	private float rotationY;//[SerializeField] 
	
	private Vector3 mouseInput;
	private Vector3 cameraFinalRotation;
	
	private GameObject player;
	

    void Awake()
    {
		//cameraTransform = transform;
        player = GameObject.FindGameObjectWithTag("Player");
		mouseSensitivity = player.GetComponent<customPlayerController3D>().GetMouseSensitivity();
		CursorLock();
    }

    void FixedUpdate()
    {
		CameraRotation();
		CameraPosition();
        PlayerRotation();
		CameraCollision();
		TempExit();
		//MoveVectorToRotation();
    }
	
	/*public Transform GetCameraTransform()
	{
		return cameraTransform;
	}*/
	
	private void CameraPosition()
	{
		Vector3 offset = transform.rotation * cameraOffset;
		transform.position = player.transform.position + offset;
	}
	
	private void PlayerRotation()
	{
		Quaternion cameraRotation = transform.rotation;
		player.transform.rotation = Quaternion.Euler(0,cameraFinalRotation.y,0);
	}
	
	private void CameraRotation()
	{
		rotationX -= Input.GetAxisRaw("Mouse Y");
		rotationY += Input.GetAxisRaw("Mouse X");
		
		rotationX = Mathf.Clamp(rotationX, minimumRotationX, maximumRotationX);
		
		cameraFinalRotation = new Vector3(rotationX, rotationY, 0) * mouseSensitivity;
		
		transform.localRotation = Quaternion.Euler(cameraFinalRotation);
	}
	
	private void CameraCollision()
	{
		Vector3 origin = player.transform.position + new Vector3(0, cameraOffset.y, 0);
		Vector3 direction = transform.rotation * Vector3.back;
		RaycastHit hit;
		
		//Debug.DrawRay(origin, direction * ray, Color.red);
		
		if(Physics.Raycast(origin, direction, out hit, ray, cameraCollisionMask))
		{
			
			float hitDistance = hit.distance;
			transform.position = transform.rotation * new Vector3(0, cameraOffset.y, -hitDistance) + player.transform.position ;
			//print(direction);
			
		}
	}
	
	private void CursorLock()
	{
		Cursor.lockState = CursorLockMode.Locked;
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