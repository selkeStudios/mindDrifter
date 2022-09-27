using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    private CheckpointManager cpm;
    private bool got = false;

    // Start is called before the first frame update
    void Start()
    {
        cpm = FindObjectOfType<CheckpointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (got == false)
        {
            cpm.UpdateCheckpoint(transform.position, other.gameObject);
            got = true;
        }
    }
}
