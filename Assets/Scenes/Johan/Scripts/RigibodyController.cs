using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigibodyController : MonoBehaviour
{
    private Vector3 inputVector;
    private Rigidbody rigidbody;
    private Animator anim;
    private float animSpeed;
    private float animDirection;

    private float rotateHorizontalSpeed;
    private float rotateVerticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal") * 10f, rigidbody.velocity.y, Input.GetAxisRaw("Vertical") * 10f);
        rigidbody.velocity = inputVector;


        float h = rotateHorizontalSpeed * Input.GetAxis("Mouse X");
        float v = rotateVerticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(v, h, 0);



        animSpeed = Input.GetAxis("Vertical");
        animDirection = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", animSpeed);
        anim.SetFloat("Direction", animDirection);

    }
}
