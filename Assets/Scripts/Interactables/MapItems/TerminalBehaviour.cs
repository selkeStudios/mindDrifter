using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalBehaviour : MonoBehaviour, IInteractable
{
    private TerminalManager tm;

    public float leeWay;

    private bool active;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        tm = transform.parent.GetComponent<TerminalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (leeWay > timer && active == true)
            {
                timer += Time.deltaTime;
            }
            else
            {
                tm.active--;
                active = false;
            }
        }
    }

    public void Interact()
    {
        if (!active)
        {
            tm.active++;
            active = true;
        }

        timer = 0;
    }
}
