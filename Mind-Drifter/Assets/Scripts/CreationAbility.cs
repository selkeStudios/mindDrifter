using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationAbility : MonoBehaviour
{
    //Array of primitives
    public GameObject[] shapes;
    public int selectedShape;

    public Camera cam;

    //Keycodes
    public KeyCode create = KeyCode.C;
    public KeyCode hold = KeyCode.V;
    public KeyCode shapeChange = KeyCode.B;
    public KeyCode scaleUp = KeyCode.LeftBracket;
    public KeyCode scaleDown = KeyCode.RightBracket;
    public KeyCode scaleX = KeyCode.LeftControl;
    public KeyCode scaleY = KeyCode.LeftAlt;
    public KeyCode scaleZ = KeyCode.LeftShift;

    //Held object data
    public GameObject obj;
    public Rigidbody objRB;
    public Vector3 objPos = new Vector3(2, 0, 0);
    public Vector3 objScale = Vector3.one;
    public Quaternion objRot = Quaternion.identity;

    public bool objHeld;

    public float scaleSens;
    public float rotSens;

    // Start is called before the first frame update
    void Start()
    {
        objRot = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Create object and hold it differently than "usual"
        if (Input.GetKeyDown(create))
        {
            HoldObj(Instantiate(shapes[selectedShape]));
        }

        //Rotate object left and right
        if (objHeld)
        {
            objRot = Quaternion.Euler(0, Input.mouseScrollDelta.y * scaleSens, 0);
            obj.transform.localRotation *= objRot;
        }

        //
        if (Input.GetKeyDown(shapeChange))
        {
            SwapObject();
        }

        //Scale object
        if (Input.GetKey(scaleUp) && !Input.GetKey(scaleDown))
        {
            ScaleObj(scaleUp);
        }
        else if (Input.GetKey(scaleDown) && !Input.GetKey(scaleUp))
        {
            ScaleObj(scaleDown);
        }

        //Pickup/set down an object
        if (Input.GetKeyDown(hold) && !objHeld)
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.distance <= 1 && hit.collider.tag == "CreativeObject")
            {
                HoldObj(hit.collider.gameObject);
            }
        }
        else if (Input.GetKeyDown(hold))
        {
            obj.transform.parent = null;

            objRB.constraints = RigidbodyConstraints.None;

            objHeld = false;

            objScale = Vector3.one;
            objRot = Quaternion.identity;
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

    //Scales the object based on what dimensions a player is trying to scale
    void ScaleObj(KeyCode key)
    {
        int dir;
        float x;
        float y;
        float z;
        Vector3 deltaScale;

        if (key == scaleUp)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }

        if (Input.GetKey(scaleX) || Input.GetKey(scaleY) || Input.GetKey(scaleZ))
        {
            if (Input.GetKey(scaleX))
            {
                x = scaleSens * dir * Time.deltaTime;
            }
            else
            {
                x = 0;
            }

            if (Input.GetKey(scaleY))
            {
                y = scaleSens * dir * Time.deltaTime;
            }
            else
            {
                y = 0;
            }

            if (Input.GetKey(scaleZ))
            {
                z = scaleSens * dir * Time.deltaTime;
            }
            else
            {
                z = 0;
            }

            deltaScale = new Vector3(x, y, z);
        }
        else
        {
            deltaScale = Vector3.one * dir * scaleSens * Time.deltaTime;
        }

        objScale += deltaScale;
        obj.transform.localScale = objScale;
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
