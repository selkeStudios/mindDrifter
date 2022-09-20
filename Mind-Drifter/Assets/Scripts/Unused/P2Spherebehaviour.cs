using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Spherebehaviour : MonoBehaviour
{
    private Rigidbody rb;
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/P2Spherebehaviour.cs
    private GameController gc;

=======

    private bool isFroze;

    private GameController gc;
>>>>>>> Stashed changes:Assets/Scripts/P2Spherebehaviour.cs
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gc = GameObject.FindObjectOfType<GameController>();
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/P2Spherebehaviour.cs
=======

        isFroze = false;
>>>>>>> Stashed changes:Assets/Scripts/P2Spherebehaviour.cs
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(3, 0, -127);
        }
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/P2Spherebehaviour.cs
=======

        if (isFroze)
        {
            gc.ActivatePlayer2Door();
        }
>>>>>>> Stashed changes:Assets/Scripts/P2Spherebehaviour.cs
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player1Goal"))
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/P2Spherebehaviour.cs
=======
            isFroze = true;
>>>>>>> Stashed changes:Assets/Scripts/P2Spherebehaviour.cs
        }
    }
}
