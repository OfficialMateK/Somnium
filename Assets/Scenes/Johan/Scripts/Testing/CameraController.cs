using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Transform cameraTarget;
    public Transform playerBody;

    private float mouseX;
    private float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(cameraTarget);

        cameraTarget.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        playerBody.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
