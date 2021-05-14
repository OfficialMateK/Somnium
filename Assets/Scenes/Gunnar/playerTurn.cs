using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTurn : MonoBehaviour
{
    /*public float RotationSpeed = 1;
    public float minYAngle;
    public float maxYAngle;
    
    float mouseX, mouseY;*/
    public Transform UpperBody;
    bool inDoorArea = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inDoorArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inDoorArea = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inDoorArea)
        {
            UpperBody.LookAt(this.gameObject.transform);
        }
            
    }

    /*private void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;

        mouseY = Mathf.Clamp(mouseY, minYAngle, maxYAngle);

        transform.LookAt(UpperBody);

        UpperBody.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }*/
}
