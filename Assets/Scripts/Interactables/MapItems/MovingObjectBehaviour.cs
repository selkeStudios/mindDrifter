using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectBehaviour : MonoBehaviour, IInteractable
{
    private Collider col;
    private Vector3 initial;

    public Vector3 axis;
    public bool positive;
    public float moveSpeed;
    public float maxDistance;
    public float leeWay;

    public int dir;
    public bool active = false;
    public float position = 0;

    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        dir = positive ? 1 : -1;
        axis = axis.normalized;
        col = GetComponentInChildren<BoxCollider>();
        initial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (timer > 0)
            {
                timer = 0;
            }

            if (position < maxDistance)
            {
                transform.position += axis * moveSpeed * dir * Time.deltaTime;
                position += moveSpeed * Time.deltaTime;
            }
            else if (position > maxDistance)
            {
                transform.position = initial + axis * maxDistance * dir;
                position = maxDistance;
            }
        }
        else 
        {
            if (position != 0)
            {
                if (timer >= leeWay)
                {
                    if (position > 0)
                    {
                        transform.position -= axis * moveSpeed * dir * Time.deltaTime;
                        position -= moveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        transform.position = initial;
                        position = 0;
                        timer = 0;
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
    }

    public void Interact()
    {
        active = !active;
    }
}
