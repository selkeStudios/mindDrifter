using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Spherebehaviour : MonoBehaviour
{
    private Rigidbody rb;

    private bool isFroze;

    private GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gc = GameObject.FindObjectOfType<GameController>();

        isFroze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(3, 0, -127);
        }

        if (isFroze)
        {
            gc.ActivatePlayer2Door();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player1Goal"))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
            isFroze = true;
        }
    }
}