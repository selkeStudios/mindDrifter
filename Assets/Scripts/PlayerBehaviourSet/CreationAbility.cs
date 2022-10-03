using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationAbility : MonoBehaviour
{
    // Array of primitives
    public GameObject[] shapes;
    public List<GameObject> created = new List<GameObject>();
    public int selectedShape = 0;

    public MovementBehaviour mb;
    public GameController gc;
    public Camera cam;
    public ModTelekinesis tk;

    // Keycodes
    public KeyCode create = KeyCode.C;
    public KeyCode hold = KeyCode.V;
    public KeyCode changePos = KeyCode.Mouse1;
    public KeyCode changeNeg = KeyCode.Mouse0;
    public KeyCode destroyShape = KeyCode.B;
    public KeyCode xRot = KeyCode.LeftAlt;
    public KeyCode zRot = KeyCode.LeftShift;

    // Held object data
    public GameObject obj;
    public Rigidbody objRB;
    public Vector3 objPos = new Vector3(0, 0, 2);

    public float rotSpeed;

    private int dir;

    void Awake()
    {
        mb = GetComponent<MovementBehaviour>();
        gc = FindObjectOfType<GameController>();
        cam = GetComponentInChildren<Camera>();
        tk = FindObjectOfType<ModTelekinesis>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mb.canMove)
        {
            dir = (Input.GetKeyDown(changePos) ? 1 : 0) + (Input.GetKeyDown(changeNeg) ? -1 : 0);
            // Changes the selected object, held or non-existent (yet)
            if (dir != 0)
            {
                SwapObject();
            }

            // Pickup object
            if (Input.GetKeyDown(hold))
            {
                if (obj == null)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.distance <= 5 && hit.collider != null && hit.collider.tag == "CreativeObject")
                    {
                        HoldObj(hit.collider.gameObject);
                    }
                }
                else
                {
                    DropObj();
                }
            }

            // Destroy object
            if (Input.GetKeyDown(destroyShape) && created.Count > 0)
            {
                if (obj != null)
                {
                    DestroyObj(obj);
                }
                else
                {
                    DestroyObj(created[created.Count - 1]);
                }
            }

            if (obj != null)
            {
                // Rotate object
                if (obj.transform.parent == cam.transform)
                {
                    RotateObj();
                }
                else
                {
                    DropObj();
                }
            }
            else
            {
                // Create object and hold it differently than "usual"
                if (Input.GetKeyDown(create))
                {
                    HoldObj(Instantiate(shapes[selectedShape]));
                    created.Add(obj);
                }
            }
        }
    }

    // Does the work to "hold an object"
    void HoldObj(GameObject obj_)
    {
        obj = obj_.gameObject;
        gc.child[1] = obj;
        objRB = obj.GetComponent<Rigidbody>();
        obj.transform.parent = cam.transform;

        obj.transform.localPosition = objPos;

        objRB.constraints = RigidbodyConstraints.FreezeAll;
    }
    
    // Drop object
    void DropObj()
    {
        if (obj.transform.parent == cam.transform)
        {
            obj.transform.parent = null;
            objRB.constraints = RigidbodyConstraints.None;
        }

        gc.child[1] = null;
        obj = null;
        objRB = null;
    }

    // Rotates the object
    void RotateObj()
    {
        Vector3 dir_;

        if (Input.GetKey(xRot))
        {
            dir_ = cam.transform.right;
        }
        else if (Input.GetKey(zRot))
        {
            dir_ = cam.transform.forward;
        }
        else
        {
            dir_ = cam.transform.up;
        }

        obj.transform.Rotate(dir_, rotSpeed * Time.deltaTime * Input.mouseScrollDelta.y, Space.World);
    }

    private int index;

    // Changes the item desired (held or palette)
    void SwapObject()
    {
        selectedShape += dir;

        if (selectedShape >= shapes.Length)
        {
            selectedShape = 0;
        }
        else if (selectedShape < 0)
        {
            selectedShape = shapes.Length - 1;
        }

        if (obj != null)
        {
            index = created.IndexOf(obj);
            created.Remove(obj);
            Destroy(obj);
            HoldObj(Instantiate(shapes[selectedShape]));
            created.Insert(index, obj);
        }
    }

    // Properly destroys created objects
    void DestroyObj(GameObject obj_)
    {
        if (obj_ == obj)
        {
            DropObj();
        }

        created.Remove(obj_);
        Destroy(obj_);
    }
}
