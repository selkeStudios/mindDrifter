//This scirpt handles all code that is not directly tied to an immediate object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] player = new GameObject[2];
    public MovementBehaviour[] mb = new MovementBehaviour[2];
    public bool talking = false;
    public bool player1 = true;

    public AudioSource MindSwap;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        player[0] = GameObject.Find("Logic").transform.GetChild(0).gameObject;
        player[1] = GameObject.Find("Creative").transform.GetChild(0).gameObject;
        mb[0] = player[0].transform.parent.GetComponent<MovementBehaviour>();
        mb[1] = player[1].transform.parent.GetComponent<MovementBehaviour>();

        player[1].SetActive(false);
        mb[1].canMove = false;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        SwitchBodies();
    }

    public void SwitchBodies()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //MindSwap.Play();

            for (int x = 0; x < 2; x++)
            {
                player[x].SetActive(!player[0].activeSelf);
                mb[x].canMove = !mb[0].canMove;
            }

            player1 = !player1;
        }
    }
}
