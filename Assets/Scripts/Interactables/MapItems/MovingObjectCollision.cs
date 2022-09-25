using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectCollision : MonoBehaviour
{
    private MovingObjectBehaviour mb;

    void Start()
    {
        mb = transform.parent.GetComponent<MovingObjectBehaviour>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform.parent);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
}
