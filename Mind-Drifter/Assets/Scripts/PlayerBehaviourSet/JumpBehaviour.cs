using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public CollisionBehaviour cb;

    /// <summary>
    /// Jump shit
    /// </summary>
    public float jumpForce = 200f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cb = GetComponent<CollisionBehaviour>();
    }

    /// <summary>
    /// Frames
    /// </summary>
    void Update()
    {
        Jump();
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
            if (cb.grounded == true)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
