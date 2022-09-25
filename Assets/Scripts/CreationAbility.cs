using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationAbility : MonoBehaviour
{
    //Array of primitives
    public GameObject[] shapes;
    public int selectedShape = 0;

    public MovementBehaviour mb;
    public Camera cam;

    //Keycodes
    public KeyCode create = KeyCode.C;
    public KeyCode hold = KeyCode.V;
    public KeyCode shapeChange = KeyCode.B;

    //Held object data
    public GameObject obj;
    public Rigidbody objRB;
    public Vector3 objPos = new Vector3(0, 0, 2);
    public Vector3 objScale = Vector3.one;
    public Quaternion objRot = Quaternion.identity;

    public bool objHeld = false;

    public float scaleSens;
    public float rotSens;

    void Awake()
    {
        mb = GetComponent<MovementBehaviour>();
        cam = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        objRot = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (mb.canMove)
        {
            //Changes the selected object, held or non-existent (yet)
            if (Input.GetKeyDown(shapeChange))
            {
                SwapObject();
            }

            if (objHeld)
            {
                //Mark object as no longer held
                if (obj.transform.parent != cam.transform)
                {
                    objHeld = false;
                    obj = null;
                }

                //Drop object
                if (Input.GetKeyDown(hold))
                {
                    obj.transform.parent = null;

                    objRB.constraints = RigidbodyConstraints.None;

                    objHeld = false;

                    objScale = Vector3.one;
                    objRot = Quaternion.identity;
                }

                if (obj != null)
                {
                    //Rotate object left and right
                    objRot = Quaternion.Euler(0, Input.mouseScrollDelta.y * scaleSens, 0);
                    obj.transform.localRotation *= objRot;
                }
            }
            else if (!objHeld)
            {
                //Create object and hold it differently than "usual"
                if (Input.GetKeyDown(create))
                {
                    HoldObj(Instantiate(shapes[selectedShape]));
                }

                //Pickup object
                if (Input.GetKeyDown(hold))
                {
                    RaycastHit hit;

                    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.distance <= 5 && hit.collider != null && hit.collider.tag == "CreativeObject")
                    {
                        HoldObj(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    //Does the work to "hold an object"
    void HoldObj(GameObject obj_)
    {
        obj = obj_.gameObject;
        objRB = obj.GetComponent<Rigidbody>();
        obj.transform.SetParent(cam.transform);

        obj.transform.localPosition = objPos;
        obj.transform.localRotation = objRot;

        objRB.constraints = RigidbodyConstraints.FreezeAll;

        objHeld = true;
    }

    //Changes the item desired (held or palette)
    void SwapObject()
    {
        if (selectedShape < shapes.Length - 1)
        {
            selectedShape++;
        }
        else
        {
            selectedShape = 0;
        }

        if (objHeld)
        {
            Destroy(obj);

            HoldObj(Instantiate(shapes[selectedShape]));
        }
    }
}
