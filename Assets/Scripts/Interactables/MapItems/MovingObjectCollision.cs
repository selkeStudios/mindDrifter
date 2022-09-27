using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent == null)
        {
            collision.transform.SetParent(transform.parent);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.parent == transform.parent)
        {
            collision.transform.SetParent(null);
        }
    }
}
