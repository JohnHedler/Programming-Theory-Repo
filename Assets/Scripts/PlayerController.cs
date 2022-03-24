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

    private Rigidbody playerRb;
    private Transform playerCamera;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCamera = Camera.main.transform;
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
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        mouseVerticalInput = Input.GetAxis("Mouse Y");
        mouseHorizontalInput = Input.GetAxis("Mouse X");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bunny")
        {
            Bunny bunnyScript = collision.gameObject.GetComponent<Bunny>();
            bunnyScript.GetDescription();
        }

        if (collision.gameObject.tag == "Duck")
        {
            Duck duckScript = collision.gameObject.GetComponent<Duck>();
            duckScript.GetDescription();
        }
    }
}
