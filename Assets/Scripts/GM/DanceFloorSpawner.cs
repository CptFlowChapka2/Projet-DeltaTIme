using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DanceFloorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gridTile;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    private GridParameters gridParameters;
    private List<TileScript> tilesP1 = new List<TileScript>();
    private List<TileScript> tilesP2 = new List<TileScript>();
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
                Vector3 posP1 = new Vector3(i, transform.position.y, j);
                Vector3 posP2 = new Vector3(i + gridSize + 1, transform.position.y, j);
                CreateTile(posP1, i, j, tilesP1);
                CreateTile(posP2, i, j, tilesP2);
            }
        }
    }

    private void CreateTile(Vector3 pos, int i, int j, List<TileScript> tiles)
    {
        TileScript tile = Instantiate(gridTile, pos, Quaternion.identity).GetComponent<TileScript>();
        tile.Initialize(i, j);
        tiles.Add(tile);
    }
}