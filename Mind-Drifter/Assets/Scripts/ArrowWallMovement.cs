using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWallMovement : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Blocker")
        {
            Destroy(gameObject);
        }
    }
}
