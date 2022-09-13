//This script controls the camera.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public Transform playerBody;
    public GameObject CurrentPlayer;
    public float sensitivity=100f;
    public float xRot;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        CurrentPlayer = Player1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        playerBody = CurrentPlayer.transform;

        Movement();
    }

    public void Movement()
    {
        float MX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float MY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= MY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * MX);
    }

  
}
