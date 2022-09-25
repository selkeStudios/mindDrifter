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
    private string check_ = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(co) && other.name.Contains(check) && loaded == null)
        {
            loaded = other.gameObject;
            rb = loaded.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            loaded.transform.parent = transform;
            loaded.transform.localPosition = loadPos;
        }
        else if (other.gameObject.GetComponent<Rigidbody>() && !other.name.Contains(check_))
        {
            Rigidbody rb_ = other.gameObject.GetComponent<Rigidbody>();
            other.gameObject.transform.parent = null;
            rb_.constraints = RigidbodyConstraints.None;
            rb_.AddForce(transform.forward * rejected, ForceMode.Impulse);
        }
    }

    public void Interact()
    {
        if (loaded != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            loaded.transform.parent = null;
            loaded.AddComponent<ProjectileBehaviour>();
            rb.AddForce(transform.forward * fire, ForceMode.Impulse);
            rb = null;
            loaded = null;
        }
    }
}
