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

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        RaycastHit hit;

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

        else
        {
            DropObj();
        }

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
