using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;
    private float mouseHorizontalInput;
    private float mouseVerticalInput;

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
    private GameObject crosshair;
    private UIHandler userInterface;

    public Projectile projectilePrefab;
    public GameObject firePoint;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCamera = Camera.main.transform;
        inspectRaycast = Camera.main.GetComponent<InspectRaycast>();
        crosshair = GameObject.Find("Crosshair");
        userInterface = GameObject.Find("UI Handler").GetComponent<UIHandler>();
    }

    private void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        if (jumped)
        {
            playerRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumped = false;
        }

        GroundCheck();
    }

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

        //move player forward/back, side-step left/right, look left/right
        transform.Translate(Vector3.forward * moveSpeed * verticalInput * Time.deltaTime);
        transform.Translate(Vector3.right * moveSpeed * horizontalInput * Time.deltaTime);
        transform.Rotate(Vector3.up * lookSensitivity * mouseHorizontalInput * Time.deltaTime);

        //tilt camera up and down, preventing further rotation past a set point
        mouseXRotation += lookSensitivity * mouseVerticalInput * Time.deltaTime;
        mouseXRotation = Mathf.Clamp(mouseXRotation, -maxLookRotation, maxLookRotation);
        playerCamera.localEulerAngles = new Vector3(-mouseXRotation, 0, 0);

        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
        }
    }

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
}
