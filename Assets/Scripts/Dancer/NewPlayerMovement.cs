using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class NewPlayerMovement : MonoBehaviour
{
    private GameObject gm;
    private GridParameters gridParameters;
    private InputPerPlayer inputPerPlayer;
    private UnityEvent<int> playerMooved=new UnityEvent<int>();
    private int gridSize;
    public int thisPLayer;

    private void Start()
    {
        gm = GameObject.Find("GM");
        gridParameters = gm.GetComponent<GridParameters>();
        inputPerPlayer = GetComponent<InputPerPlayer>();
        InitialisedLocalEvents();
    }

    private void Update()
    {
        MoveOnGrid();
        ClampingOnGrid();
    }

    private void MoveOnGrid()
    {
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x + inputPerPlayer.movement.x, pos.y, pos.z + inputPerPlayer.movement.y);
        transform.position = pos;
    }

    private void ClampingOnGrid()
    {
        int offset = 0;

        if (this.name == "Dancer2")
        {
            offset = (int)gridParameters.gridSize + 1;
        }
        
        if (transform.position.x >= gridParameters.gridSize + offset)
        {
            transform.position = new Vector3(gridParameters.gridSize + offset - 1, transform.position.y, transform.position.z);
            inputPerPlayer.numberOfRightThisMeasure--;
        }
        else if (transform.position.x < offset)
        {
            transform.position = new Vector3(offset, transform.position.y, transform.position.z);
            inputPerPlayer.numberOfLeftThisMeasure--;
        }

        if (transform.position.z >= gridParameters.gridSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, gridParameters.gridSize - 1);
            inputPerPlayer.numberOfUpThisMeasure--;
        }
        else if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            inputPerPlayer.numberOfDownThisMeasure--;
        }
    }

    private void InitialisedLocalEvents()
    {
        playerMooved.AddListener(FindAnyObjectByType<EventSyncroniser>().ReceivePlayerMoove);
    }
}
