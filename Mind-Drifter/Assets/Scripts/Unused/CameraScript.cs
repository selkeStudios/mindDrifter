//This script controls the camera.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/CameraScript.cs
    public GameObject Player1;
    public GameObject Player2;
    public Transform playerBody;
    public GameObject CurrentPlayer;
    public float sensitivity=100f;
    public float xRot;

=======

   public GameObject Player1;
   public GameObject Player2;
    public Transform playerBody;
     GameObject CurrentPlayer;
    public float sensitivity=100f;
    public float xRot;
>>>>>>> Stashed changes:Assets/Scripts/CameraScript.cs
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        CurrentPlayer = Player1;
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/CameraScript.cs
        Cursor.lockState = CursorLockMode.Locked;
    }

=======
    }
>>>>>>> Stashed changes:Assets/Scripts/CameraScript.cs
    private void Update()
    {
        playerBody = CurrentPlayer.transform;

        Movement();
<<<<<<< Updated upstream:Mind-Drifter/Assets/Scripts/Unused/CameraScript.cs
    }

    public void Movement()
    {
=======
        
    }

   

    public void Movement()
    {

>>>>>>> Stashed changes:Assets/Scripts/CameraScript.cs
        float MX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float MY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= MY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * MX);
    }

  
}
