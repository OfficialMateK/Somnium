using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float RotationSpeed = 1;
    public float minYAngle;
    public float maxYAngle;
    public Transform PlayerHead;

    float mouseX, mouseY;

    private void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;

        mouseY = Mathf.Clamp(mouseY, minYAngle, maxYAngle);

        transform.LookAt(PlayerHead);

        PlayerHead.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
