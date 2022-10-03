using UnityEngine;

public class ModTelekinesis : MonoBehaviour
{
    public Camera cam;
    public GameController gc;
    public MovementBehaviour mb;
    public CreationAbility ca;

    public KeyCode hold = KeyCode.V;
    public KeyCode modify = KeyCode.C;
    public KeyCode orient = KeyCode.B;
    public KeyCode push = KeyCode.Mouse1;
    public KeyCode pull = KeyCode.Mouse0;
    public KeyCode xRot = KeyCode.LeftAlt;
    public KeyCode zRot = KeyCode.LeftShift;

    public GameObject[] objSet = new GameObject[4];
    public GameObject[] objMods = new GameObject[4];
    public string[] objs = {"SquarePyramid", "Cube", "Cylinder", "Sphere"};
    public LayerMask pickupLayer;

    private GameObject obj;
    private Rigidbody objRB;

    private float objDist;
    private int moveDir;
    public float minDist = 2;

    private Vector3 oldPos;

    public float rotSpeed = 100;
    public float moveSpeed = 10;

    void Awake()
    {
        mb = GetComponent<MovementBehaviour>();
        gc = FindObjectOfType<GameController>();
        cam = GetComponentInChildren<Camera>();
        ca = FindObjectOfType<CreationAbility>();
    }

    void Update()
    {
        // Disabled when shouldn't be able to move.
        if (mb.canMove)
        {
            if (Input.GetKeyDown(hold))
            {
                // If nothing held, holds object
                if (obj == null)
                {
                    RaycastHit hit;

                    // Uses camera position to check for objects in pickup layer, LMB press/hold holds object + release lets go
                    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, pickupLayer))
                    {
                        HoldObj(hit.transform.gameObject);
                    }
                }
                // Else it drops said object
                else
                {
                    DropObj();
                }
            }

            // If an object is held...
            if (obj != null)
            {
                // And if that object is properly parented...
                if (obj.transform.parent == cam.transform)
                {
                    if (Input.GetKeyDown(modify))
                    {
                        ModifyShape();
                    }

                    // Converts keys into 1s or 0s so they work in math equations for simplification
                    // bool ? ifTrue : ifFalse
                    moveDir = (Input.GetKey(push) ? 1 : 0) * 1 + (Input.GetKey(pull) ? 1 : 0) * -1;

                    if (moveDir != 0)
                    {
                        MoveObj();
                    }

                    RotateObj();

                    if (Input.GetKeyDown(orient))
                    {
                        obj.transform.localRotation = Quaternion.identity;
                    }

                    oldPos = obj.transform.position;
                }
                // If not parented correctly, the obj isn't held
                else
                {
                    DropObj();
                }
            }
        }
    }

    // Does the work to "hold an object"
    void HoldObj(GameObject obj_)
    {
        obj = obj_.gameObject;
        gc.child[0] = obj;
        objRB = obj.GetComponent<Rigidbody>();
        obj.transform.SetParent(cam.transform);

        objDist = (transform.position - obj.transform.position).magnitude;
        obj.transform.localPosition = Vector3.forward * objDist;

        objRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Resets all the changed components in HoldObj
    void DropObj()
    {
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

    // Moves object forward or back
    void MoveObj()
    {
        if (objDist > minDist || moveDir > 0)
        {
            objDist += moveSpeed * moveDir * Time.deltaTime;
            obj.transform.localPosition = Vector3.forward * objDist;
        }
        else if (objDist < minDist)
        {
            objDist = minDist;
            obj.transform.localPosition = Vector3.forward * objDist;
        }
    }

    // Rotates the object
    void RotateObj()
    {
        Vector3 dir;

        if  (Input.GetKey(xRot))
        {
            dir = cam.transform.right;
        }
        else if (Input.GetKey(zRot))
        {
            dir = cam.transform.forward;
        }
        else
        {
            dir = cam.transform.up;
        }

        obj.transform.Rotate(dir, rotSpeed * Time.deltaTime * Input.mouseScrollDelta.y, Space.World);
    }

    private GameObject newObj;
    private int index;

    // Modifies the shape
    void ModifyShape()
    {
        // Loops through objs to find a matching shape to swap
        for (int x = 0; x < objs.Length; x++)
        {
            if (obj.name.Contains(objs[x]))
            {
                // Checks if name containts TK, like TK Cube, the platform obj
                if (obj.name.Contains("TK"))
                {
                    newObj = Instantiate(objSet[x], obj.transform.position, obj.transform.rotation);
                }
                else if (objMods[x] != null)
                {
                    newObj = Instantiate(objMods[x], obj.transform.position, obj.transform.rotation);
                }
                else
                {
                    break;
                }

                if (newObj != null)
                {
                    index = ca.created.IndexOf(obj);
                    ca.created.Remove(obj);
                    Destroy(obj);
                    DropObj();
                    HoldObj(newObj);
                    ca.created.Insert(index, obj);
                    break;
                }
            }
        }
    }
}
