using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRBehaviour : MonoBehaviour
{
    /// <summary>
    /// Personal Settings
    /// </summary>
    public float sensitivity = 200f;

    public bool paused = false;

    public int hori;
    public int vert;

    /// <summary>
    /// The basics
    /// </summary>
    public Camera cam;
    public Rigidbody rb;
    public CapsuleCollider coll;

    public Vector3 standOffset = new Vector3(0f, 0.5369999f, 0f);
    public Vector3 crouchOffset = Vector3.zero;
    public Vector3 camOffset;

    /// <summary>
    /// Grounding Shit
    /// </summary>
    public bool grounded = false;
    public PhysicMaterial normal;
    public PhysicMaterial slip;
    public ContactPoint[] contacts;
    public Vector3 groundNormal;
    public Vector3 point;
    public Vector3 curveCenterBottom;
    public Vector3 curveCenterTop;

    /// <summary>
    /// Movement Shit
    /// </summary>
    public Vector3 moveDir;
    public float moveForce;
    public float walkSpeed = 10f;
    public float sprintSpeed = 20f;
    public float crouchSpeed = 5f;
    public float speedCap = 10f;

    public Vector3 finalMove;

    /// <summary>
    /// Jump shit
    /// </summary>
    public float jumpForce = 200f;
    public bool jumped = false;
    public bool wallHop;
    public Vector3 wallNormal;
    public Vector3 perpRight;
    public Vector3 perpLeft;

    public bool crouched = false;

    /// <summary>
    /// Ledge maneuver shit
    /// </summary>
    public float ledgeDistance;
    public float climbSpeed;
    public RaycastHit ledgeCheck;

    /// <summary>
    /// Runs once, at start
    /// </summary>
    void Start()
    {
        cam = Camera.main;
        coll = gameObject.GetComponent<CapsuleCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Frames
    /// </summary>
    void Update()
    {
        vert = (Input.GetKey(KeyCode.W) ? 1 : 0) * 1 + (Input.GetKey(KeyCode.S) ? 1 : 0) * -1;
        hori = (Input.GetKey(KeyCode.D) ? 1 : 0) * 1 + (Input.GetKey(KeyCode.A) ? 1 : 0) * -1;


        moveDir = (transform.right * hori + transform.forward * vert).normalized;
        CameraMovement();
        Crouch();
        Jump();
    }

    /// <summary>
    /// Update, but for physics reliant items
    /// </summary>
    void FixedUpdate()
    {
        if (grounded == true)
        {
            coll.material = normal;
        }
        else
        {
            coll.material = slip;
        }

        if (moveDir != Vector3.zero)
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

    /// <summary>
    /// Alters camera height and collider height to simulate crouching
    /// </summary>
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
        else
        {
            speedCap = walkSpeed;
        }

        //Apply speed
        if (rb.velocity.magnitude < speedCap && grounded)
        {
            finalMove = Vector3.ProjectOnPlane(moveDir, groundNormal);
        }
        else if (wallHop)
        {
            float right = Vector3.Angle(transform.forward, perpRight);
            float left = Vector3.Angle(transform.forward, perpLeft);

            if (right != left)
            {
                if(right > left)
                {
                    finalMove = perpLeft;
                }
                else
                {
                    finalMove = perpRight;
                }
            }
        }

        rb.AddForce(finalMove * moveForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Used to jump or jump off walls (when possible)
    /// 
    /// Should be considered incomplete, there must be a better way to wall hop
    /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Regular jump
            if (grounded == true)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            //Wallhop
            else if (wallHop == true)
            {
                Quaternion correction = Quaternion.identity;

                Vector3 left = Quaternion.AngleAxis(10f, Vector3.up) * perpLeft;
                Vector3 right = Quaternion.AngleAxis(-10f, Vector3.up) * perpRight;
                float angleSize = Vector3.Angle(left, right);

                float angleLeft = Vector3.Angle(transform.forward, left);
                float angleRight = Vector3.Angle(right, transform.forward);
                float angleTLeft = Vector3.Angle(perpLeft, transform.forward);
                float angleTRight = Vector3.Angle(transform.forward, perpRight);

                Vector3 hopDir = Quaternion.AngleAxis(45, -transform.right) * transform.forward;

                //Angle depth correction
                if (angleLeft + angleRight > angleSize)
                {
                    if (angleTLeft < 20)
                    {
                        correction = Quaternion.AngleAxis(angleLeft, Vector3.up);
                    }
                    else if (angleTRight < 20)
                    {
                        correction = Quaternion.AngleAxis(angleRight, Vector3.down);
                    }
                    else
                    {
                        correction = Quaternion.AngleAxis(180, Vector3.up);
                    }
                }

                rb.AddForce(correction * hopDir.normalized * jumpForce * 1.5f, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// Calls GroundCheck, inserting its contact points
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);
        GroundCheck(contacts);
    }

    /// <summary>
    /// Use in case groundcheck failure
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (grounded == false)
        {
            contacts = new ContactPoint[collision.contactCount];
            collision.GetContacts(contacts);
            GroundCheck(contacts);
        }
    }

    /// <summary>
    /// Use in case ground detection failure
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        //Truely ungrounded
        if (collision.contactCount == 0)
        {
            grounded = false;
            wallHop = false;
        }
        //Double check
        else
        {
            contacts = new ContactPoint[collision.contactCount];
            collision.GetContacts(contacts);
            GroundCheck(contacts);
        }
    }

    /// <summary>
    /// Script used to find a grounding or wall hop-off point
    /// </summary>
    /// <param name="contacts_">
    /// Contacts gathered when OnCollisionEnter is called
    /// </param>
    void GroundCheck(ContactPoint[] contacts_)
    {
        point = Vector3.zero;
        groundNormal = Vector3.zero;
        wallHop = false;
        jumped = true;

        curveCenterBottom = coll.bounds.center - Vector3.up * (coll.bounds.extents.y - coll.radius);
        curveCenterTop = coll.bounds.center + Vector3.up * (coll.bounds.extents.y - coll.radius);

        foreach (ContactPoint c in contacts_)
        {
            Vector3 dir = curveCenterBottom - c.point;
            Vector3 dir2 = c.point - curveCenterTop;

            //Ground detect
            if (dir.y > 0f && Mathf.Abs(Vector3.Angle(c.normal, Vector3.up)) <= 40)
            {
                groundNormal = c.normal;

                grounded = true;
                jumped = false;
            }
            //Wall check
            else if (dir2.y < 0f)
            {
                wallNormal = c.normal;

                perpLeft = Vector3.Cross(wallNormal, Vector3.up);
                perpRight = Vector3.Cross(Vector3.up, wallNormal);

                wallHop = true;
            }
        }
    }
}
