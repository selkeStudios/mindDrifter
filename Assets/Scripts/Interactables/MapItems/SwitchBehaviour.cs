using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour, IInteractable
{
    public GameObject[] objs;
    private IInteractable[] ib;

    private bool activated = false;

    void Start()
    {
        ib = new IInteractable[objs.Length];

        for (int x = 0; x < objs.Length; x++)
        {
            ib[x] = objs[x].GetComponent<IInteractable>();
        }
    }

    public void Interact()
    {
        if (!activated)
        {
            for (int x = 0; x < ib.Length; x++)
            {
                ib[x].Interact();
            }
        }
    }
}
