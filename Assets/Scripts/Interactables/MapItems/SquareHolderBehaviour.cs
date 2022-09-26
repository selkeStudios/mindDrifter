using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareHolderBehaviour : MonoBehaviour
{
    public GameObject obj;
    public Rigidbody objRB;

    private string tag_ = "CreativeObject";
    private string name_ = "Cube";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_) && other.gameObject.name.Contains(name_))
        {
            obj = other.gameObject;
            obj.transform.parent = null;
            objRB = obj.GetComponent<Rigidbody>();

            objRB.constraints = RigidbodyConstraints.FreezeAll;

            obj.transform.localScale = transform.localScale;
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;

            obj.layer = 0;
            obj.tag = "Untagged";
        }
    }
}
