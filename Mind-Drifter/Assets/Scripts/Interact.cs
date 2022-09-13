using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameController gc;
    public Camera cam;

    public float reach;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.distance <= reach)
            {
                Press(hit.collider.gameObject);
            }
        }
    }

    public void Press(GameObject obj)
    {
        
    }
}