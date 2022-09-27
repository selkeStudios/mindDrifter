using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Vector3[] checkpoints = new Vector3[2];
    private GameObject[] players = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        players[0] = GameObject.Find("Logic");
        players[1] = GameObject.Find("Creative");
        checkpoints[0] = players[0].transform.position;
        checkpoints[1] = players[1].transform.position;
    }

    public void UpdateCheckpoint(Vector3 cp, GameObject player)
    {
        if (player == players[0])
        {
            checkpoints[0] = cp;
        }
        else if (player == players[1])
        {
            checkpoints[1] = cp;
        }
    }

    public void ReturnPlayer(GameObject player)
    {
        if (player == players[0])
        {
            player.transform.position = checkpoints[0];
        }
        else if (player == players[1])
        {
            player.transform.position = checkpoints[1];
        }
    }
}
