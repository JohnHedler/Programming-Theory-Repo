using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player input variables
    private float verticalInput;
    private float horizontalInput;
    private float mouseHorizontalInput;
    private float mouseVerticalInput;

    //player movement/look variables
    private float moveSpeed = 5.0f;
    private float runSpeed = 8.0f;
    private float walkSpeed = 5.0f;
    private float lookSensitivity = 180.0f;
    private float jumpHeight = 5.0f;
    private float distToGround = 1.0f;
    private float mouseXRotation = 0;
    private float maxLookRotation = 45;

    private bool isOnGround = true;
    private bool jumped = false;

    private bool inspectOn = false;

    private Rigidbody playerRb;
    private Transform playerCamera;
    private InspectRaycast inspectRaycast;

    //UI variables
    private GameObject crosshair;
    private UIHandler userInterface;

    //combat variables
    public Projectile projectilePrefab;
    public GameObject firePoint;

    //out of bounds
    private DeathSystem deathSystem;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCamera = Camera.main.transform;
        inspectRaycast = Camera.main.GetComponent<InspectRaycast>();
        crosshair = GameObject.Find("Crosshair");
        userInterface = GameObject.Find("UI Handler").GetComponent<UIHandler>();
        deathSystem = GetComponent<DeathSystem>();
    }

    private void Update()
    {
        GetInputs();

        PlayerOutOfBounds();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    //GetInputs function; receives all user input assigned and performs tasks associated with them.
    private void GetInputs()
    {
        //assign user inputs
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        mouseVerticalInput = Input.GetAxis("Mouse Y");
        mouseHorizontalInput = Input.GetAxis("Mouse X");

        //change player's run speed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
        }

        //shoot projectiles
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
        }

        //enable/disable inspect mode
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inspectOn)
            {
                inspectOn = true;
                inspectRaycast.inspectOn = true;
            }
            else
            {
                inspectOn = false;
                inspectRaycast.inspectOn = false;
            }

            inspectRaycast.CrosshairChangeImage();
        }

        //turn on/off tutorial panel
        if (Input.GetKeyDown(KeyCode.L))
        {
            userInterface.ToggleTutorial();
        }

        //move player forward/back, side-step left/right, turn left/right
        transform.Translate(Vector3.forward * moveSpeed * verticalInput * Time.deltaTime);
        transform.Translate(Vector3.right * moveSpeed * horizontalInput * Time.deltaTime);
        transform.Rotate(Vector3.up * lookSensitivity * mouseHorizontalInput * Time.deltaTime);

        //tilt camera up and down, preventing further rotation past a set point
        mouseXRotation += lookSensitivity * mouseVerticalInput * Time.deltaTime;
        mouseXRotation = Mathf.Clamp(mouseXRotation, -maxLookRotation, maxLookRotation);
        playerCamera.localEulerAngles = new Vector3(-mouseXRotation, 0, 0);

        //allow player to jump
        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
        }
    }

    //Jump function; when jumped is set to true, add force to rigidbody of player so it jumps
    private void Jump()
    {
        if (jumped)
        {
            playerRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumped = false;
        }

        GroundCheck();
    }

    //GroundCheck function; check to see if player is not in the air to enable jumping.
    //  If player is in the air, jumping is disabled.
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    private void PlayerOutOfBounds()
    {
        if(transform.position.y < -5)
        {
            deathSystem.Defeated();
        }
    }
}
