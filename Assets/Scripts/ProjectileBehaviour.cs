using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private string tag_ = "Destructible";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(tag_))
        {
            Destroy(collision.gameObject);
            Destroy(this);
        }
    }
}
