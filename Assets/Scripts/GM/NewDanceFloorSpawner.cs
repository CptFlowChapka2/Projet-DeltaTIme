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

    private List<KeyValuePair<Vector2Int, NewTileScript>> allInvalideTileP1 =new List<KeyValuePair<Vector2Int, NewTileScript>>() ;
    private Dictionary<Vector2Int,NewTileScript> tilesP2 = new Dictionary<Vector2Int, NewTileScript>();
    private List<KeyValuePair<Vector2Int, NewTileScript>> allInvalideTileP2 =new List<KeyValuePair<Vector2Int, NewTileScript>>() ;
    
    
    public int gridSize;

    private void Start()
    {
        gridParameters = GetComponent<GridParameters>();
        gridSize = (int)gridParameters.gridSize+2;
        SpawnGrid();
        
        MakeExtremityTileInvalid(tilesP1,allInvalideTileP1);
        MakeExtremityTileInvalid(tilesP2,allInvalideTileP2);
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        int halfSize = (int)Math.Floor((float)gridSize / 2);
        
        NewPlayerMovement p1=player1.GetComponent<NewPlayerMovement>();
        NewPlayerMovement p2=player2.GetComponent<NewPlayerMovement>();
        
        p1.currentTile = tilesP1[new Vector2Int(halfSize, halfSize)];
        p2.currentTile = tilesP2[new Vector2Int(halfSize, halfSize)];

        p1.transform.position = 
            new Vector3(p1.currentTile.transform.position.x,p1.transform.position.y,p1.currentTile.transform.position.z);
        p2.transform.position = 
            new Vector3(p2.currentTile.transform.position.x,p2.transform.position.y,p2.currentTile.transform.position.z);

        p1.tiles = tilesP1;
        p2.tiles = tilesP2;
        
        p1.invalideTile = allInvalideTileP1;
        p2.invalideTile = allInvalideTileP2;
        
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


    private void MakeExtremityTileInvalid( Dictionary<Vector2Int,NewTileScript> tiles,
        List<KeyValuePair<Vector2Int, NewTileScript>> allInvalideTile)
    {
        allInvalideTile = tiles.Where(y =>
                y.Key.x.Equals(0) || y.Key.x.Equals(gridSize - 1) || y.Key.y.Equals(0) || y.Key.y.Equals(gridSize - 1))
            .ToList();
        allInvalideTile.ForEach(x=>x.Value.thisState=NewTileScript.TileState.Invalid);
    }
}