//This scirpt handles all code that is not directly tied to an immediate object.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject darkwall1;
    public GameObject darkwall2;
    public GameObject darkwall3;
    public GameObject darkwall4;
    public GameObject darkwallroof;
    public GameObject Player1Cam;
    public GameObject Player2Cam;
  public static GameObject CurrentPlayer;
    public GameObject[] InvisWalls;

    public GameObject arrowWall;

    public GameObject mainArrowWall;

    public GameObject hiddenPlatform1;
    public GameObject hiddenPlatform2;

    public GameObject mazeBlocker1;
    public GameObject mazeBlocker2;
    public GameObject mazeBlocker3;

    public Transform arrowWallSpawner;
    public Transform player1Checkpoint;
    public Transform player2Checkpoint;

    public GameObject player1Door;
    public GameObject player2Door;

    public AudioSource MindSwap;
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        InvisWalls = GameObject.FindGameObjectsWithTag("Invisible Walls");
        foreach (GameObject GO in InvisWalls)
        {
            GO.GetComponent<MeshRenderer>().enabled = true;
        }
        Cursor.visible = false;
        CurrentPlayer = Player1Cam;
        Player2Cam.SetActive(false);

        InvokeRepeating("ArrowWallSpawner", 1.0f, 3.0f);
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

    public void CloseDarkRoom()
    {
        darkwall1.SetActive(false);
        darkwall2.SetActive(false);
        darkwall3.SetActive(false);
        darkwall4.SetActive(false);
        darkwallroof.SetActive(false);
    }
    public void CloseArrowWall()
    {
        mainArrowWall.SetActive(false);
        CancelInvoke();
    }

    public void DeactivateMazeBlocker1()
    {
        mazeBlocker1.SetActive(false);
    }

    public void DeactivateMazeBlocker2()
    {
        mazeBlocker2.SetActive(false);
    }

    public void DeactivateMazeBlocker3()
    {
        mazeBlocker3.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(4);
    }

    public void ActivateHiddenPlatform()
    {
        hiddenPlatform1.SetActive(true);
        hiddenPlatform2.SetActive(true);
    }

    public void ActivatePlayer1Door()
    {
        player1Door.SetActive(true);
    }

    public void ActivatePlayer2Door()
    {
        player2Door.SetActive(true);
    }

    public void ArrowWallSpawner()
    {
        Instantiate(arrowWall, arrowWallSpawner.position, Quaternion.identity);
    }

    public void SwitchBodies() //doesn't currently work
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MindSwap.Play();

            if (CurrentPlayer == Player1Cam)
            {
                Player2Cam.SetActive(true);
                CurrentPlayer = Player2Cam;
                Player1Cam.SetActive (false);
                foreach (GameObject GO in InvisWalls)
                {
                    GO.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            else if (CurrentPlayer == Player2Cam)
            {
                Player1Cam.SetActive(true);
                CurrentPlayer = Player1Cam;
                Player2Cam.SetActive(false);
                foreach (GameObject GO in InvisWalls)
                {
                    GO.GetComponent<MeshRenderer>().enabled = false;
                }

            }

            

        }
    }

    
}
