using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour, IInteractable
{
    public GameObject loaded;

    public Vector3 loadPos;
    public float rejected;
    public float fire;

    private Rigidbody rb;

    private string co = "CreativeObject";
    private string check = "Sphere";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(co) && other.name.Contains(check))
        {
            loaded = other.gameObject;
            rb = loaded.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            loaded.transform.parent = transform;
            loaded.transform.localPosition = loadPos;
        }
        else if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * rejected, ForceMode.Impulse);
        }
    }

    public void Interact()
    {
        if (loaded != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            loaded.transform.parent = null;
            rb.AddForce(transform.forward * fire, ForceMode.Impulse);
            rb = null;
            loaded = null;
        }
    }
}
