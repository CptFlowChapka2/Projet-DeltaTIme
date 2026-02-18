using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewDanceFloorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gridTile;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    private GridParameters gridParameters;
    private Dictionary<Vector2Int,NewTileScript> tilesP1 = new Dictionary<Vector2Int, NewTileScript>();
    private Dictionary<Vector2Int,NewTileScript> tilesP2 = new Dictionary<Vector2Int, NewTileScript>();
    
    private int gridSize;

    private void Start()
    {
        gridParameters = GetComponent<GridParameters>();
        gridSize = (int)gridParameters.gridSize+2;
        SpawnGrid();
        SpawnPlayers();
        MakeExtremityTileInvalid(tilesP1);
        MakeExtremityTileInvalid(tilesP2);
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
                Vector3 posP2 = new Vector3(i + gridSize , transform.position.y, j);
                CreateTile(posP1, i, j, tilesP1);
                CreateTile(posP2, i, j, tilesP2);
            }
        }
    }

    private void CreateTile(Vector3 pos, int i, int j, Dictionary<Vector2Int,NewTileScript> tiles)
    {
        NewTileScript tile = Instantiate(gridTile, pos, Quaternion.identity).GetComponent<NewTileScript>();
        tile.Initialize(i, j);
        tiles.Add(new Vector2Int(i,j),tile);
    }


    private void MakeExtremityTileInvalid( Dictionary<Vector2Int,NewTileScript> tiles)
    {
        tiles.Where(y=>
            y.Key.x.Equals(0)||y.Key.x.Equals(gridSize)||y.Key.y.Equals(0)||y.Key.y.Equals(gridSize))
            .ToList().ForEach(x=>x.Value.thisState=NewTileScript.TileState.Invalid);
    }
}