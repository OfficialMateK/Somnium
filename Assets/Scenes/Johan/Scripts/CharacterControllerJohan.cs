using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerJohan : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 verticalVelocity;
    private float horizontalInput;
    private float verticalInput;
    private float groundedTimer;
    private bool isGrounded;
    Vector3 movement;

    //Animation
    private Animator animator;
    private float animatorSpeed;
    private float animatorDirection;



    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);


        //CheckGravity();
        //CheckMovement();
        
        //ApplyMovement();


        // animatorSpeed = Input.GetAxis("Vertical");
        //animatorDirection = Input.GetAxis("Horizontal");

    }

    /*
    
    private void CheckGravity()
    {
        isGrounded = controller.isGrounded;


        if (isGrounded)
        {
            groundedTimer = 0.2f;
        }

        if(groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        if(isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        //controller.Move(verticalVelocity * Time.deltaTime);
    }

    private void CheckMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && groundedTimer > 0)
        {
            groundedTimer = 0f;
            verticalVelocity += Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        //Flyttar karaktären framåt, bakåt och åt sidorna
        //Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        //Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        // controller.Move(move * moveSpeed * Time.deltaTime);


    }

    private void ApplyMovement()
    {
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        move *= moveSpeed;

        if(move.magnitude > 0.05f)
        {
            gameObject.transform.forward = move;
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
        //controller.Move(moveSpeed * Time.deltaTime * movement + verticalVelocity * Time.deltaTime);
    }
    */
    
}
