using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    public GameController gc;
    public MovementBehaviour mb;
    public DialogueManager dm;
    public Camera cam;

    public float reach;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        dm = FindObjectOfType<DialogueManager>();
        mb = GetComponent<MovementBehaviour>();
    }

    void LateUpdate()
    {
        if (mb.canMove)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (gc.talking)
                {
                    dm.Interact();
                }
                else
                {
                    RaycastHit hit;

                    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.distance <= reach)
                    {
                        IInteractable ib = hit.collider.gameObject.GetComponent<IInteractable>();

                        if (ib != null)
                        {
                            ib.Interact();
                        }
                    }
                }
            }
        }
    }
}