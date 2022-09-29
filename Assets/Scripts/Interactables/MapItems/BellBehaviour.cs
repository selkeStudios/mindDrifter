using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellBehaviour : MonoBehaviour
{
    public GameObject[] objs;
    public IInteractable[] ib;

    private string tag_ = "CreativeObject";

    void Start()
    {
        ib = new IInteractable[objs.Length];

        for (int x = 0; x < objs.Length; x++)
        {
            ib[x] = objs[x].GetComponent<IInteractable>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tag_) && collision.gameObject.name.Contains("Cylinder"))
        {
            foreach (IInteractable ii in ib)
            {
                ii.Interact();
            }
        }
    }
}
