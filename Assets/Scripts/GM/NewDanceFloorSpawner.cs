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
    private NewTileScript[,] tilesP1;
    private NewTileScript[,] tilesP2;
    private List< NewTileScript> allInvalideTileP1 =new List<NewTileScript>() ;
    private List< NewTileScript> allInvalideTileP2 =new List<NewTileScript>() ;
   
    
    
    public int gridSize;

    private void Start()
    {
        gridParameters = GetComponent<GridParameters>();
        gridSize = (int)gridParameters.gridSize+2;
        
        tilesP1=InitialiseGridArray();
        tilesP2=InitialiseGridArray();
        SpawnGrid();
        
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        int halfSize = (int)Math.Floor((float)gridSize / 2);
        
        NewPlayerMovement p1=player1.GetComponent<NewPlayerMovement>();
        NewPlayerMovement p2=player2.GetComponent<NewPlayerMovement>();
        
        p1.currentTile = tilesP1[halfSize, halfSize];
        p2.currentTile = tilesP2[halfSize, halfSize];

        p1.transform.position = 
            new Vector3(p1.currentTile.transform.position.x,p1.transform.position.y,p1.currentTile.transform.position.z);
        p2.transform.position = 
            new Vector3(p2.currentTile.transform.position.x,p2.transform.position.y,p2.currentTile.transform.position.z);

        p1.thatPLayerGrid = tilesP1;
        p2.thatPLayerGrid = tilesP2;
        
        p1.invalideTile = allInvalideTileP1;
        p2.invalideTile = allInvalideTileP2;
        
    }

    private void SpawnGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Vector3 posP1 = new Vector3(i, transform.position.y, j);
                Vector3 posP2 = new Vector3(i + gridSize , transform.position.y, j);
                CreateTile(posP1, i, j, tilesP1,allInvalideTileP1);
                CreateTile(posP2, i, j, tilesP2,allInvalideTileP2);
                
            }
        }
        
    }

    private void CreateTile(Vector3 pos, int i, int j, NewTileScript[,] tiles,List< NewTileScript> invalidTileList)
    {
        NewTileScript tile = Instantiate(gridTile, pos, Quaternion.identity).GetComponent<NewTileScript>();
        tile.Initialize(i, j);
        tiles[i,j]=tile;
        if ((i == 0 || j == 0) || (i == gridSize - 1 || j == gridSize - 1)) //check if tile is an extremity
        {
            invalidTileList.Add(tile);
            tile.thisState = NewTileScript.TileState.Invalid;
        }
        
    }


    private NewTileScript[,]  InitialiseGridArray()
    {
        return new NewTileScript[gridSize, gridSize];
    }
    
   
}