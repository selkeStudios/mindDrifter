//This scirpt handles all code that is not directly tied to an immediate object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] player = new GameObject[2];
    public bool player1 = true;

    public AudioSource MindSwap;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        player[1].SetActive(false);
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        SwitchBodies();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SwitchBodies()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MindSwap.Play();

            player[0].SetActive(!player[0].activeSelf);
            player[1].SetActive(!player[0].activeSelf);
            player1 = !player1;
        }
    }
}
