using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1BoxBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private GameController gc;

    private bool isFroze;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        isFroze = false;

        gc = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -6)
        {
            transform.position = new Vector3(-6, 6, -124);
        }

        if (isFroze)
        {
            gc.ActivatePlayer1Door();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player2Goal"))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
            isFroze = true;
        }
    }
}
