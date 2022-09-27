using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    public Camera cam;
    public GameController gc;
    public MovementBehaviour mb;
    private GameObject obj;
    private Rigidbody objRB;
    public LayerMask pickupLayer;
    private Vector3 objPos;
    private float objDist;
    public float minDist = 2;
    private Vector3 oldPos;

    public float scrollSens = 100;

    private bool objHeld = false;

    void Awake()
    {
        mb = GetComponent<MovementBehaviour>();
        gc = FindObjectOfType<GameController>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (mb.canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                //uses camera position to check for objects in pickup layer, LMB press/hold holds object + release lets go
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, pickupLayer))
                {
                    HoldObj(hit.transform.gameObject);
                }
            }
            else if (objHeld && Input.GetMouseButtonUp(0))
            {
                DropObj();
            }

            if (obj != null)
            {
                if (obj.transform.parent == cam.transform)
                {
                    float scroll = Input.mouseScrollDelta.y;

                    if (scroll != 0 && (objDist >= minDist || Input.mouseScrollDelta.y > 0))
                    {
                        objDist = objPos.magnitude + scroll * scrollSens;
                        objPos = objPos.normalized * objDist;
                        obj.transform.localPosition = objPos;
                    }

                    oldPos = obj.transform.position;
                }
                else
                {
                    DropObj();
                }
            }
        }
    }

    //Does the work to "hold an object"
    void HoldObj(GameObject obj_)
    {
        obj = obj_.gameObject;
        gc.child[0] = obj;
        objRB = obj.GetComponent<Rigidbody>();
        obj.transform.SetParent(cam.transform);

        objDist = (transform.position - obj.transform.position).magnitude;
        obj.transform.localPosition = Vector3.forward * objDist;
        objPos = obj.transform.localPosition;

        objRB.constraints = RigidbodyConstraints.FreezeAll;

        objHeld = true;
    }

    // resets all the changed components in HoldObj
    void DropObj()
    {
        objHeld = false;

        if (obj.transform.parent == cam.transform)
        {
            obj.transform.parent = null;
            objRB.constraints = RigidbodyConstraints.None;
            objRB.velocity = obj.transform.position - oldPos;
        }

        objRB = null;
        gc.child[0] = null;
        obj = null;
    }
}
