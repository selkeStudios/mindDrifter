using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingedObjectBehaviour : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Need to be setup in window
    /// 
    /// What axis does it rotate around?
    /// Does it open positively or negatively according to this axis?
    /// How far is the hinge in that direction (in % of door bounds)?
    /// How fast does it open in degrees per second?
    /// </summary>

    public Vector3 axis;
    public bool positive;
    public float rotationSpeed;
    public float maxRotation;

    private bool activating = false;
    private int dir;
    private float rotation = 0;

    void Start()
    {
        dir = positive ? 1 : -1;
    }

    void Update()
    {
        if (activating)
        {
            if (rotation < maxRotation)
            {
                transform.Rotate(axis * rotationSpeed * dir * Time.deltaTime);
                rotation += rotationSpeed * Time.deltaTime;
            }
            else if (rotation > maxRotation)
            {
                activating = false;
                transform.Rotate(axis * -(rotation - maxRotation) * dir);
                rotation = maxRotation;
            }
            else
            {
                activating = false;
            }
        }
    }

    public void Interact()
    {
        activating = true;
    }
}
