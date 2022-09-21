using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
    public GameObject[] objs;
    private IInteractable[] ib;

    public int active;

    // Start is called before the first frame update
    void Start()
    {
        ib = new IInteractable[objs.Length];

        for (int x = 0; x < objs.Length; x++)
        {
            ib[x] = objs[x].GetComponent<IInteractable>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active > 1)
        {
            for (int x = 0; x < objs.Length; x++)
            {
                ib[x].Interact();
            }
        }
    }
}
