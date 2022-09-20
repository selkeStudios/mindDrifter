using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectBehaviour : MonoBehaviour, IInteractable
{
    private Transform parent;
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
    private bool falling = false;

    // Start is called before the first frame update
    void Start()
    {
        dir = positive ? 1 : -1;
        axis = axis.normalized;
        parent = transform.parent;
        initial = parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (position < maxDistance)
            {
                parent.position += axis * moveSpeed * dir * Time.deltaTime;
                position += moveSpeed * Time.deltaTime;
            }
            else if (position > maxDistance)
            {

                parent.position += axis * maxDistance * dir;
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
                        parent.position -= axis * moveSpeed * dir * Time.deltaTime;
                        position -= moveSpeed * Time.deltaTime;
                    }
                    else if (position < 0)
                    {
                        parent.position = initial;
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
        collision.transform.SetParent(parent);
    }

    private void OnCollisionExit(Collision collision)
    {
        Transform hit = collision.transform;
        hit.SetParent(null);
        collision.gameObject.GetComponent<Rigidbody>().velocity += axis * dir * moveSpeed;
    }

    public void Interact()
    {
        active = !active;
    }
}
