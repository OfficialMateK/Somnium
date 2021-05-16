using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float RotationSpeed = 1;
    public float minYAngle;
    public float maxYAngle;
    public Transform PlayerHead;
    
    //[SerializeField] private float rayLength;

    //[SerializeField] private Vector3 cameraOffset;

    //[SerializeField] private LayerMask cameraCollisionMask;

    private GameObject player;
    


    float mouseX, mouseY;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;

        mouseY = Mathf.Clamp(mouseY, minYAngle, maxYAngle);

        transform.LookAt(PlayerHead);

        PlayerHead.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        
    }

    /*private void FixedUpdate()
    {
        //CameraCollision();
    }

    private void CameraCollision()
	{
        Vector3 origin = PlayerHead.transform.position;// + new Vector3(0, 0, 0);
		Vector3 direction = PlayerHead.rotation * Vector3.back;
		RaycastHit hit;

		Debug.DrawRay(origin, direction * rayLength, Color.red);

		if (Physics.Raycast(origin, direction, out hit, rayLength, cameraCollisionMask))
		{

			float hitDistance = hit.distance;
			transform.position = transform.rotation * new Vector3(0, 0, -hitDistance) + player.transform.position;
			//print(direction);

		}
	}*/
}
