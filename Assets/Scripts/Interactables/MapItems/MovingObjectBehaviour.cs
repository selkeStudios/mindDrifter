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

    private int dir;
    private bool active = false;
    private float position = 0;

    private float timer = 0;

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
                    else if (position < 0)
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

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform.GetChild(0));
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);

        if (active)
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity += axis * dir * moveSpeed;
        }
        else if (timer >= leeWay)
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity -= axis * dir * moveSpeed;
        }
    }

    public void Interact()
    {
        active = !active;
    }
}
