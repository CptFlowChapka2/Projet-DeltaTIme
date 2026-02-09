using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DanceFloorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gridTile;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    private GridParameters gridParameters;
    private int gridSize;

    private void Start()
    {
        gridParameters = GetComponent<GridParameters>();
        gridSize = (int)gridParameters.gridSize;
        SpawnGrid();
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        float halfSize = (gridParameters.gridSize / 2f) - 0.5f;
        player1.transform.position = new Vector3(halfSize, transform.position.y, halfSize);
        player2.transform.position = new Vector3(gridSize + halfSize + 1, transform.position.y, halfSize);
    }

    private void SpawnGrid()
    {
        for (int i = 0; i < gridParameters.gridSize; i++)
        {
            for (int j = 0; j < gridParameters.gridSize; j++)
            {
                Instantiate(gridTile, new Vector3(i, transform.position.y, j), Quaternion.identity);
                Instantiate(gridTile, new Vector3(i + gridSize + 1, transform.position.y, j), Quaternion.identity);
            }
        }
    }
}
