using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1BoxBehaviour : MonoBehaviour
{
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/P1BoxBehaviour.cs
    //Don't think this is used?s

    private Rigidbody rb;
    private GameController gc;

=======
    private Rigidbody rb;
    private GameController gc;

    private bool isFroze;
>>>>>>> Stashed changes:Assets/Scripts/P1BoxBehaviour.cs
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/P1BoxBehaviour.cs
=======
        isFroze = false;

>>>>>>> Stashed changes:Assets/Scripts/P1BoxBehaviour.cs
        gc = GameObject.FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -6)
        {
            transform.position = new Vector3(-6, 6, -124);
        }
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/P1BoxBehaviour.cs
=======

        if (isFroze)
        {
            gc.ActivatePlayer1Door();
        }
>>>>>>> Stashed changes:Assets/Scripts/P1BoxBehaviour.cs
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player2Goal"))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/P1BoxBehaviour.cs
=======
            isFroze = true;
>>>>>>> Stashed changes:Assets/Scripts/P1BoxBehaviour.cs
        }
    }
}
