using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividerBehaviour : MonoBehaviour
{
    private string tag_ = "CreativeObject";

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (!other.gameObject.CompareTag(tag_))
        {
            print("reversed");
            rb.velocity = -rb.velocity;
        }
    }
}
