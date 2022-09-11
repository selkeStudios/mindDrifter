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
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        CanMove();

        Movement();

        gc = GameObject.FindObjectOfType<GameController>();

    }

    /// <summary>
    /// Detects the beginning of collision.
    /// </summary>
    /// <param name="collision">The object collided with.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "lightswitch")
        {
            gc.CloseDarkRoom();

            TaskComplete.Play();
        }

        if(collision.gameObject.name == "Platform Button")
        {
            gc.ActivateHiddenPlatform();

            TaskComplete.Play();
        }

        if(collision.gameObject.name.Contains("Arrow wall") && gameObject.CompareTag("Player1"))
        {
          //  Destroy(collision.gameObject);
          gameObject.transform.position = gc.player1Checkpoint.transform.position;
            print(gc.player1Checkpoint.position);

        }

        if(collision.gameObject.name == "Fire" && gameObject.CompareTag("Player2"))
        {
            gameObject.transform.position = gc.player2Checkpoint.position;
        }

       if(collision.gameObject.name == "Player 2" && gameObject.CompareTag("Player1"))
        {
            gc.MainMenu();

            TaskComplete.Play();
        }

        if (collision.gameObject.name == "Player 1" && gameObject.CompareTag("Player2"))
        {
            gc.MainMenu();

            TaskComplete.Play();
        }
      
        if (collision.gameObject.name == "Stopper Plate")
        {
            gc.CloseArrowWall();

            TaskComplete.Play();
        }
        
        if(collision.gameObject.name == "MB1")
        {
            gc.DeactivateMazeBlocker1();

            TaskComplete.Play();
        }

        if (collision.gameObject.name == "MB2")
        {
            gc.DeactivateMazeBlocker2();

            TaskComplete.Play();
        }

        if (collision.gameObject.name == "MB3")
        {
            gc.DeactivateMazeBlocker3();

            TaskComplete.Play();
        }

        if (collision.gameObject.CompareTag("Player2Door") && gameObject.CompareTag("Player2"))
        {
            transform.position = new Vector3(-14, 0, -160);
            print(collision);
        }

        if (collision.gameObject.CompareTag("Player1Door") && gameObject.CompareTag("Player1"))
        {
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Detects when collision is continuous.
    /// </summary>
    /// <param name="collision">The object collided with.</param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire") && gameObject.CompareTag("Player2"))
        {
            transform.position = gc.player2Checkpoint.position;
        }

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
            Vector3 move = transform.right * x*Time.deltaTime + transform.forward * z*Time.deltaTime;
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

    /// <summary>
    /// Detects and sets whether the player should be able to move or not.
    /// </summary>
    public void CanMove()
    {
        if (GameController.CurrentPlayer.name == "Player1Cam")
        {   
            if(gameObject.tag == "Player1")
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
        }

        if (GameController.CurrentPlayer.name == "Player2Cam")
        {
            if(gameObject.tag == "Player2")
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
        }

    }

}
