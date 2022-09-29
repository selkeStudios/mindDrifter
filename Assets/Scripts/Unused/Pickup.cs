using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform itemHolderLeft, itemHolderRight;
 
    private Transform carriedObjectRight, carriedObjectLeft;
 
    public LayerMask pickupLayer;

    public float pickUpDist;
  
    public float throwForceOneHand, throwForceTwoHand;

    public Transform leftArmRot;

    public Transform rightArmRot;

    public Transform twoHandedSpot;

    // Reference to Player Controller script
    PlayerControl pc;

    Rigidbody objectRBLeft;
    Rigidbody objectRBRight;
    Rigidbody objectRBBoth;


    private void Start()
    {
        pc = gameObject.GetComponent<PlayerControl>();
    }

    /// <summary>
    /// Checks for inputs for picking up an objects and dropping them.
    /// Also checks inputs for tilting held objects.
    /// </summary>
    void Update()
    {
        // Pick Up and Drop
        if (Input.GetKeyDown(KeyCode.Q))// && pc.canMove)
        {
            if (carriedObjectLeft)
                Drop(carriedObjectLeft);
            else
                PickUp(itemHolderLeft);
        }
        if (Input.GetKeyDown(KeyCode.E))// && pc.canMove)
        {
            if (carriedObjectRight)
                Drop(carriedObjectRight);
            else
                PickUp(itemHolderRight);
        }
    }

    /// <summary>
    /// Drops objects in left or right hand.
    /// </summary>
    /// <param name="hand"> Carried Object </param>
    void Drop(Transform hand)
    {
        // Check if you are carrying two handed object
        if (carriedObjectLeft && carriedObjectLeft.Equals(carriedObjectRight))
        {
            // Throw the object in front of player
            if (!carriedObjectRight.GetComponent<Rigidbody>())
            {
                //carriedObjectRight.GetComponent<Rigidbody>().isKinematic = false;
                carriedObjectRight.gameObject.AddComponent<Rigidbody>();
                Rigidbody newObjRB = carriedObjectLeft.GetComponent<Rigidbody>();
                newObjRB = objectRBBoth;
                if (carriedObjectRight.gameObject.name == "Exit Crate")
                {
                    carriedObjectLeft.GetComponent<Rigidbody>().mass = 2f;
                }
                carriedObjectRight.GetComponent<Rigidbody>().AddForce(carriedObjectRight.forward * throwForceTwoHand);
            }
            if (!carriedObjectLeft.GetComponent<Rigidbody>())
            {
                //carriedObjectLeft.GetComponent<Rigidbody>().isKinematic = false;
                //carriedObjectLeft.GetComponent<Rigidbody>().AddForce(carriedObjectLeft.forward * throwForceTwoHand);
            }

            // Set objects back to "Pickup" layer and remove parent from their transforms
            carriedObjectLeft.gameObject.layer = 6;
            carriedObjectRight.parent = null;
            carriedObjectLeft = null;
            carriedObjectRight = null;
        }
        // Right Hand
        else if (hand.Equals(carriedObjectRight))
        {
            // Throw object
            if (carriedObjectRight.GetComponent<Rigidbody>())
            {
                carriedObjectRight.GetComponent<Rigidbody>().isKinematic = false;
                carriedObjectRight.GetComponent<Rigidbody>().AddForce(carriedObjectRight.forward * throwForceOneHand);
            }

            // Set objects back to "Pickup" layer and remove parent from their transforms
            carriedObjectRight.gameObject.layer = 6;
            carriedObjectRight.parent = null;
            carriedObjectRight = null;
        }
        // Left Hand
        else if (hand.Equals(carriedObjectLeft))
        {
            // Throw object
            if (carriedObjectLeft.GetComponent<Rigidbody>())
            {
                carriedObjectLeft.GetComponent<Rigidbody>().isKinematic = false;
                carriedObjectLeft.GetComponent<Rigidbody>().AddForce(carriedObjectLeft.forward * throwForceOneHand);
            }

            // Set objects back to "Pickup" layer and remove parent from their transforms
            carriedObjectLeft.gameObject.layer = 6;
            carriedObjectLeft.parent = null;
            carriedObjectLeft = null;
        }
    }

    /// <summary>
    /// Find the closest object near the player that can be picked up and
    /// place it into the players hand or hands.
    /// </summary>
    /// <param name="hand"> Object Trying To Pick Up </param>
    void PickUp(Transform hand)
    {
        // Collect every pickup around. Make sure they have a collider and the layer Pickup
        Collider[] pickups = Physics.OverlapSphere(transform.position, pickUpDist, pickupLayer);

        // Find the closest
        float dist = Mathf.Infinity;
        Transform closest = null;
        for (int i = 0; i < pickups.Length; i++)
        {
            float newDist = Vector3.Distance(transform.position, pickups[i].transform.position);
            if (newDist < dist)
            {
                closest = pickups[i].transform;
                dist = newDist;
            }
        }

        // Check if we found something
        if (closest)
        {
            // Two handed object
            if (closest.gameObject.tag == "2")
            {
                // If player is not currently carrying anything
                if (carriedObjectLeft == null && carriedObjectRight == null)
                {
                    // Prevents rigidbody of object from moving around
                    if (closest.GetComponent<Rigidbody>())
                    {
                        //closest.GetComponent<Rigidbody>().isKinematic = true;
                        objectRBBoth = closest.GetComponent<Rigidbody>();
                        Destroy(closest.GetComponent<Rigidbody>());
                    }

                    // Pickup object's position and rotation
                    closest.parent = twoHandedSpot;
                    closest.rotation = twoHandedSpot.rotation;

                    // ----------------------------------------------------------------------------------------------
                    closest.position = twoHandedSpot.position;
                    // ----------------------------------------------------------------------------------------------

                    // Both hands are seen as having objects in them
                    carriedObjectLeft = closest;
                    carriedObjectRight = closest;

                    // Pickup layer set to "Default"
                    closest.gameObject.layer = 0;
                }
            }
            // One handed object
            else
            {
                // Prevents rigidbody of object from moving around
                if (closest.GetComponent<Rigidbody>())
                {
                    closest.GetComponent<Rigidbody>().isKinematic = true;
                }

                // Pickup object's position and rotation
                closest.rotation = transform.rotation;
                closest.parent = hand;
                closest.localPosition = new Vector3(0, 0, 0);

                // Not Sure...
                // ----------------------------------------------------------------------------------------------
                if (hand.Equals(itemHolderLeft))
                    carriedObjectLeft = closest;
                else
                    carriedObjectRight = closest;
                closest.gameObject.layer = 0;
                // ----------------------------------------------------------------------------------------------
            }

        }
    }
}
