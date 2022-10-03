using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBehaviour : MonoBehaviour
{
    public GameObject obj;
    private IInteractable ib;
    private MovingObjectBehaviour mb;

    public string co = "CreativeObject";
    public string ot = "SquarePyramid";

    void Start()
    {
        ib = obj.GetComponent<IInteractable>();

        if (ib.GetType().ToString() == "MovingObjectBehaviour")
        {
            mb = obj.GetComponent<MovingObjectBehaviour>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(co) && collision.gameObject.name.Contains(ot))
        {
            if (mb != null)
            {
                mb.active = true;
            }
            else
            {
                ib.Interact();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.contactCount == 0)
        {
            if (mb != null)
            {
                mb.active = false;
            }
            else
            {
                ib.Interact();
            }
        }
    }
}
