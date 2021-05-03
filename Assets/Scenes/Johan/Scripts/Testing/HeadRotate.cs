using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((Input.GetAxis("Mouse X") * speed * Time.deltaTime), (Input.GetAxis("Mouse Y") * speed * Time.deltaTime), 0, Space.World);
    }
}
