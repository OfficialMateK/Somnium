using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerJohan : MonoBehaviour
{
    //Movement
    private Vector3 movementDirection;
    public float moveSpeed = 12f;
    private float horizontalInput;
    private float verticalInput;

    //Dash
    private float dashSpeedMultiplier;
    public float dashSpeedMultiplerAmount = 4f;
    private float dashDuration;
    public float dashDuratationAmount = 0.11f;
    private float dashCooldown;
    public float dashCooldownAmount = 2f;
    private bool lockInputs;
    

    //Gravity and Jump
    public Transform groundCheck;
    public LayerMask groundMask;
    private Vector3 verticalVelocity;
    private bool isGrounded;
    private float groundDistance = 0.4f;
    private float groundedTimer = 0.2f;
    public float jumpHeight = 1.8f;
    public float jumpCooldown = 0.2f;  
    public float gravity = -20f;
    

    //References
    private CharacterController controller;
    private Animator animator;
    private PlayerAudio playerAudio;
    private PlayerParticles playerParticles;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<PlayerAudio>();
        playerParticles = GetComponent<PlayerParticles>();
    }


    // Update is called once per frame
    void Update()
    {
        ApplyMovement();
        ApplyGravity();
    }

    private void ApplyMovement()
    {
        //Sätter animation
        animator.SetFloat("Direction", horizontalInput);
        animator.SetFloat("Speed", verticalInput);

        //Börjar när spelaren dashar och dashen inte är på cooldown och körs tills dashens slut
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= Time.time || dashDuration > 0 && dashDuration != dashDuratationAmount)
        {
            Dash();
        }
        else
        {
            dashDuration = dashDuratationAmount;
            dashSpeedMultiplier = 1f;
            lockInputs = false;

            //Kollar spelarens input för vanlig rörelse
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }

        //Flyttar spelaren och normaliserar move så att spelaren inte rör sig snabbare diagonalt med flera knappar nedtryckta
        //Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        // move = transform.TransformDirection(move.normalized);

        GetMovementDirection(horizontalInput, verticalInput);
        controller.Move(movementDirection * Time.deltaTime * moveSpeed * dashSpeedMultiplier);

    }

    private void GetMovementDirection(float horizontalInput, float verticalInput)
    {
        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection = transform.TransformDirection(movementDirection.normalized);
    }

    private void Dash()
    {
        //"Dashar" genom att öka spelarens movespeed under dashDuration och låsa deras riktning
        dashDuration -= Time.deltaTime;
        dashCooldown = Time.time + dashCooldownAmount; //Startar dashens cooldown
        dashSpeedMultiplier = dashSpeedMultiplerAmount;
        verticalVelocity.y = 0f; //Stoppar spelarens hopp uppåt så att de inte avslutar hoppet i slutet av dashen

        //Kollar dashens riktning och låser spelaren till denna
        if (!lockInputs)
        {
            playerAudio.PlayDashSound();

            lockInputs = true;
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            GetMovementDirection(horizontalInput, verticalInput);
            playerParticles.PlayDashTrail(movementDirection);

            if (isGrounded)
            {
                playerParticles.PlayLeafParticles(movementDirection);
            }

            //Om ingen spelaren står stilla defaultar dashen till rakt frammåt
            if (horizontalInput == 0 && verticalInput == 0)
            {
                verticalInput = 1;
            }
        }
    }

    private void ApplyGravity()
    {
        //Kollar när spelaren står på marken (Ground i layer) och suger fast dem en del för att inte studsa så mycket
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Låter spelaren hoppa en kort stund efter även om de lämnat marken
        if (isGrounded)
        {
            groundedTimer = 0.2f;
            animator.SetBool("isGrounded", true);
        }
        else
        {
            groundedTimer -= Time.deltaTime;
            animator.SetBool("isGrounded", false);
        }

        if (!lockInputs)
        {
            //Vertical velocty är låst till -2 när spelaren står på marken för att suga fast dom lite och göra den mindre studsig
            if (isGrounded && verticalVelocity.y < 0)
            {
                
                verticalVelocity.y = -2f;
            }

            //När spelaren hoppar på marken ökar deras verticalVelocity
            if (Input.GetButtonDown("Jump") && groundedTimer > 0 && jumpCooldown <= Time.time)
            {
                jumpCooldown = Time.time + 0.2f;
                groundedTimer = 0;
                animator.SetTrigger("Jump");
                playerAudio.PlayJumpSound();
                verticalVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravity);
            }

            //Drar spelaren neråt över tid efter gravitationen
            verticalVelocity.y += gravity * Time.deltaTime;
            controller.Move(verticalVelocity * Time.deltaTime);
        }
    } 
}
