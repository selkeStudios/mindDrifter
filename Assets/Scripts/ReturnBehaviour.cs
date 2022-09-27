using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBehaviour : MonoBehaviour
{
    private CheckpointManager cpm;

    // Start is called before the first frame update
    void Start()
    {
        cpm = FindObjectOfType<CheckpointManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        cpm.ReturnPlayer(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        cpm.ReturnPlayer(other.gameObject);
    }
}
