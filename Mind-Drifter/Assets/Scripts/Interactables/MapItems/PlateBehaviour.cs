using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBehaviour : MonoBehaviour
{
    public GameObject obj;
    private IInteractable ib;

    private string co = "CreativeObject";

    void Start()
    {
        ib = obj.GetComponent<IInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(co))
        {
            ib.Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(co))
        {
            ib.Interact();
        }
    }
}
