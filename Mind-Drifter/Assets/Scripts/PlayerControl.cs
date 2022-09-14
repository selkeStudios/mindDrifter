// This Script controls the basic functions that both player characters will share.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public Transform findGround;
    public float speed = 12f;
    public float gravity = -10;
    public float groundDistance = 0.5f;
    public float jumpHeight = 3f;
    float x;
    float z;
    public LayerMask platformMask;
    bool isOnGround;
    public bool canMove;

    Vector3 velocity;

    private Rigidbody rb2d;

    private GameController gc;

    public AudioSource TaskComplete;


    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        rb2d = GetComponent<Rigidbody>();
        gc = FindObjectOfType<GameController>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        Movement();
    }
  
    /// <summary>
    /// Controls player movement.
    /// </summary>
    public void Movement()
    {
        isOnGround = Physics.CheckSphere(findGround.position, groundDistance, platformMask);

        if (isOnGround && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
         
        if (canMove == true)
        {
            Vector3 move = transform.right * x * Time.deltaTime + transform.forward * z * Time.deltaTime;
            move.Normalize();


            controller.Move(move * speed * Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround&&canMove)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
