using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    public GameController gc;
    public Rigidbody rb;
    public CollisionBehaviour cb;
    public CapsuleCollider coll;
    public Camera cam;
    public MenuBehaviour mb;

    public bool canMove = true;

    /// <summary>
    /// Movement Shit
    /// </summary>
    public int hori;
    public int vert;
    public Vector3 moveDir;
    public float moveForce = 10;
    public float walkSpeed = 10f;
    public float sprintSpeed = 20f;
    public float crouchSpeed = 5f;
    public float speedCap = 10f;

    public Vector3 finalMove;

    public bool crouched = false;

    public float jumpForce = 200f;

    public float sensitivity;

    /// <summary>
    /// Cam and Collider Shit
    /// </summary>
    public Vector3 standOffset = new Vector3(0f, 0.5369999f, 0f);
    public Vector3 crouchOffset = Vector3.zero;

    public Vector3 camOffset;

    // Start is called before the first frame update

    void Awake()
    {
        mb = FindObjectOfType<MenuBehaviour>();
        gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        cb = GetComponent<CollisionBehaviour>();
        coll = GetComponent<CapsuleCollider>();
        cam = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Frames
    /// </summary>
    void Update()
    {
        /*if (mb.paused)
        {
            canMove = false;
        }
        */
        if (canMove)
        {
            //Determining a movement direction vector
            vert = (Input.GetKey(KeyCode.W) ? 1 : 0) * 1 + (Input.GetKey(KeyCode.S) ? 1 : 0) * -1;
            hori = (Input.GetKey(KeyCode.D) ? 1 : 0) * 1 + (Input.GetKey(KeyCode.A) ? 1 : 0) * -1;
            moveDir = (transform.right * hori + transform.forward * vert).normalized;

            CameraMovement();
            Crouch();
            Jump();
        }
    }

    /// <summary>
    /// Update, but for physics reliant items
    /// </summary>
    void FixedUpdate()
    {
        if (canMove)
        {
            Movement();
        }
    }

    float xRotation;

    /// <summary>
    /// Controls the camera
    /// </summary>
    void CameraMovement()
    {
        xRotation = Mathf.Clamp(xRotation - Input.GetAxis("Mouse Y") * sensitivity * 50f * Time.deltaTime, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity * 50f * Time.deltaTime);
    }

    void Crouch()
    {
        //Crouch
        if (Input.GetKeyDown(KeyCode.F) && crouched == false)
        {
            coll.height /= 1.5f;
            coll.center = new Vector3(0, -0.25f, 0);
            camOffset = crouchOffset;
            crouched = true;
        }

        //Uncrouch
        if (Input.GetKeyUp(KeyCode.F) && crouched == true)
        {
            coll.height *= 1.5f;
            coll.center = new Vector3(0, 0, 0);
            camOffset = standOffset;
            crouched = false;
        }
    }

    /// <summary>
    /// Contains the script for walking based on where the player is touching the ground (needs revising for corners)
    /// </summary>
    void Movement()
    {
        //Set speed
        if (crouched == true)
        {
            speedCap = crouchSpeed;
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            speedCap = sprintSpeed;
        }
        else
        {
            speedCap = walkSpeed;
        }

        //Apply speed
        if (rb.velocity.magnitude < speedCap)
        {
            if (cb.grounded)
            {
                finalMove = Vector3.ProjectOnPlane(moveDir, cb.groundNormal);
            }
            else if (cb.wallHop)
            {
                float right = Vector3.Angle(transform.forward, cb.perpRight);
                float left = Vector3.Angle(transform.forward, cb.perpLeft);

                if (right != left)
                {
                    if (right > left)
                    {
                        finalMove = cb.perpLeft;
                    }
                    else
                    {
                        finalMove = cb.perpRight;
                    }

                    finalMove /= 4;
                }
            }
        }
        else
        {
            finalMove = Vector3.zero;
        }

        rb.AddForce(finalMove * moveForce, ForceMode.Impulse);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Regular jump
            if (cb.grounded == true)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
