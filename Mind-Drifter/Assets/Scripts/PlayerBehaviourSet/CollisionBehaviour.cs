using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehaviour : MonoBehaviour
{
    public CapsuleCollider coll;

    /// <summary>
    /// Grounding Shit
    /// </summary>
    public PhysicMaterial normal;
    public PhysicMaterial slip;
    public ContactPoint[] contacts;
    public Vector3 point;
    public Vector3 curveCenterBottom;
    public Vector3 curveCenterTop;

    /// <summary>
    /// Carried Values
    /// </summary>
    public bool grounded = false;
    public bool jumped = false;
    public bool wallHop = false;
    public Vector3 groundNormal;
    public Vector3 wallNormal;
    public Vector3 perpLeft;
    public Vector3 perpRight;

    private void Start()
    {
        coll = GetComponent<CapsuleCollider>();
    }

    /// <summary>
    /// Update, but for physics reliant items
    /// </summary>
    void FixedUpdate()
    {
        if (grounded == true)
        {
            coll.material = normal;
        }
        else
        {
            coll.material = slip;
        }
    }

    /// <summary>
    /// Calls GroundCheck, inserting its contact points
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);
        GroundCheck(contacts);
    }

    /// <summary>
    /// Use in case groundcheck failure
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (grounded == false)
        {
            contacts = new ContactPoint[collision.contactCount];
            collision.GetContacts(contacts);
            GroundCheck(contacts);
        }
    }

    /// <summary>
    /// Use in case ground detection failure
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        //Truely ungrounded
        if (collision.contactCount == 0)
        {
            grounded = false;
            wallHop = false;
        }
        //Double check
        else
        {
            contacts = new ContactPoint[collision.contactCount];
            collision.GetContacts(contacts);
            GroundCheck(contacts);
        }
    }

    /// <summary>
    /// Script used to find a grounding or wall hop-off point
    /// </summary>
    /// <param name="contacts_">
    /// Contacts gathered when OnCollisionEnter is called
    /// </param>
    void GroundCheck(ContactPoint[] contacts_)
    {
        point = Vector3.zero;
        groundNormal = Vector3.zero;
        wallHop = false;
        jumped = true;

        curveCenterBottom = coll.bounds.center - Vector3.up * (coll.bounds.extents.y - coll.radius);
        curveCenterTop = coll.bounds.center + Vector3.up * (coll.bounds.extents.y - coll.radius);

        foreach (ContactPoint c in contacts_)
        {
            Vector3 dir = curveCenterBottom - c.point;
            Vector3 dir2 = c.point - curveCenterTop;

            //Ground detect
            if (dir.y > 0f && Mathf.Abs(Vector3.Angle(c.normal, Vector3.up)) <= 40)
            {
                groundNormal = c.normal;

                grounded = true;
                jumped = false;
            }
            //Wall check
            else if (dir2.y < 0f)
            {
                wallNormal = c.normal;

                perpLeft = Vector3.Cross(wallNormal, Vector3.up);
                perpRight = Vector3.Cross(Vector3.up, wallNormal);

                wallHop = true;
            }
        }
    }
}
