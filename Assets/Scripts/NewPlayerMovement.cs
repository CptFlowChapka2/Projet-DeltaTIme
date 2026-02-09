using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewPlayerMovement : MonoBehaviour
{
    private GameObject gm;
    private GridParameters gridParameters;
    private InputReader inputReader;
    private int gridSize;

    private void Start()
    {
        gm = GameObject.Find("GM");
        gridParameters = gm.GetComponent<GridParameters>();
        inputReader = gm.GetComponent<InputReader>();
    }

    private void Update()
    {
        MoveOnGrid();
        ClampingOnGrid();
    }

    private void MoveOnGrid()
    {
        Vector2 currentMove = Vector2.zero;

        if (this.name == "Dancer1")
        {
            currentMove = new Vector2(inputReader.HorizontalMoveP1, inputReader.VerticalMoveP1);
        }
        else if (this.name == "Dancer2")
        {
            currentMove = new Vector2(inputReader.HorizontalMoveP2, inputReader.VerticalMoveP2);
        }
        
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x + currentMove.x, pos.y, pos.z + currentMove.y);
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
        }
        else if (transform.position.x < offset)
        {
            transform.position = new Vector3(offset, transform.position.y, transform.position.z);
        }

        if (transform.position.z >= gridParameters.gridSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, gridParameters.gridSize - 1);
        }
        else if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
