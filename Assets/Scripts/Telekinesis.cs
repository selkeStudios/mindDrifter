using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    public Camera cam;
    private GameObject heldObj;
    private Rigidbody heldRB;
    public LayerMask pickupLayer;
    private Transform heldParent;
    private Vector3 oldPos;
    private Vector3 scrollVel;


   
    // gets the camera
    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        RaycastHit hit;
        //uses camera position to check for objects in pickup layer, LMB press/hold holds object + release lets go
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, pickupLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                HoldObj(hit.transform.gameObject);
            }

            else if (Input.GetMouseButtonUp(0))
            {
                DropObj();
            }
        }
        // drops if nothing is being held 
        else
        {
            DropObj();
        }
        // if scroll wheel is used while holding an object moves the object closer or farther from player
        if (heldObj)
        {
            float scroll = Input.mouseScrollDelta.y;
            if (scroll != 0)
            {
                scrollVel = cam.transform.forward * scroll * 100;
            }
            else
            {
                scrollVel = scrollVel / 1.1f;
            }
            heldRB.velocity = scrollVel;

            heldRB.angularVelocity = Vector3.zero;
            oldPos = heldObj.transform.position;
        }
    }
    // removes gravity from object when being held, while holding an object sets it to a child, scrollVel allows object to move speed wise
    void HoldObj(GameObject obj)
    {
        DropObj();

        heldObj = obj;
        heldRB = obj.GetComponent<Rigidbody>();
        heldRB.useGravity = false;
        heldParent = obj.transform.parent;
        heldObj.transform.parent = cam.transform;
        scrollVel = Vector3.zero;
    }

    // resets all the changed components in HoldObj
    void DropObj()
    {
        if (heldObj != null)
        {
            heldRB.useGravity = true;
            heldObj.transform.parent = heldParent;
            Vector3 newVel = heldObj.transform.position - oldPos;
            heldRB.velocity = newVel;
            heldObj = null;
        }
    }
}
