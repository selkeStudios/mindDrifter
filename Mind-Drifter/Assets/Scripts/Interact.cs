using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject door1;

    public GameObject button1;

    public GameObject door2;

    public GameObject button2;

    public GameObject door3;

    public GameObject button3;

    public float interactDist;

    public LayerMask interactLayer;



    public void Press()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, interactDist, interactLayer);

        // Find the closest
        float dist = Mathf.Infinity;
        Transform closest = null;
        for (int i = 0; i < objects.Length; i++)
        {
            float newDist = Vector3.Distance(transform.position, objects[i].transform.position);
            if (newDist < dist)
            {
                closest = objects[i].transform;
                dist = newDist;
            }
        }

        if (closest != null)
        {
            Debug.Log(closest.name);
            if (closest.name == "button1")
            {
                door1.SetActive(false);
            }
            else if (closest.name == "button2")
            {
                door2.SetActive(false);
            }
            else if (closest.name == "button3")
            {
                door3.SetActive(false);
            }
        }

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Press();
        }
    }
}