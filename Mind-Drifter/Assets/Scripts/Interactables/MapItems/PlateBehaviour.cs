using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBehaviour : MonoBehaviour
{
    public GameObject obj;
    public IInteractable ib;

    void Start()
    {
        ib = obj.GetComponent<IInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ib.Interact();
    }

    private void OnTriggerExit(Collider other)
    {
        ib.Interact();
    }
}
