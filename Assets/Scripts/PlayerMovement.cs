using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridParameters gridParameters;
    [SerializeField] private InputReader inputReader;
    private int gridSize;

    private void Start()
    {
        gridSize = (int)gridParameters.gridSize;
        int startX = Random.Range(0, gridSize);
        int startZ = Random.Range(0, gridSize);
        transform.position = new Vector3(startX, transform.position.y, startZ);
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x + inputReader.HorizontalMove, pos.y, pos.z + inputReader.VerticalMove);
        transform.position = pos;
        ClampingOnGrid();
    }

    private void ClampingOnGrid()
    {
        if (transform.position.x >= gridParameters.gridSize)
        {
            transform.position = new Vector3(gridParameters.gridSize - 1, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
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
