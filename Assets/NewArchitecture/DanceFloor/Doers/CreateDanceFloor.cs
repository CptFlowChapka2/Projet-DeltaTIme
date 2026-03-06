using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateDanceFloor : Doer
{
    private DanceFloorManager danceFloorManager;
    private TileiD[,] allTileId = new TileiD[,]{};
    private List<TileiD> allInvalideTile = new List<TileiD>();
    private GameObject tilePrefab;
    private int danceFloorSize;

    private void Start()
    {
        danceFloorManager = (DanceFloorManager)manager;
        GetAllUsefulParameters();
       allTileId= InitialiseDanceFloorArray();
       SpawnDanceFloor();
       TeleportPLayerToCenterOfDanceFloor();
    }

    protected override void GetAllUsefulParameters()
    {
        tilePrefab = danceFloorManager.tilePrefab;
        danceFloorSize = danceFloorManager.danceFloorSize + 2;

    }
    
    private TileiD[,]  InitialiseDanceFloorArray()
    {
        return new TileiD[danceFloorSize, danceFloorSize];
    }
    
    private void SpawnDanceFloor()
    {
        for (int i = 0; i < danceFloorSize; i++)
        {
            for (int j = 0; j < danceFloorSize; j++)
            {
                Vector3 posP1 = new Vector3(i, transform.position.y, j);
                CreateTile(posP1, i, j, allTileId,allInvalideTile);
                
            }
        }
        
    }
    private void CreateTile(Vector3 pos, int i, int j, TileiD[,] tiles,List< TileiD> invalidTileList)
    {
        TileiD tile = Instantiate(tilePrefab, pos,Quaternion.identity).GetComponent<TileiD>();
        tile.Initialise(danceFloorManager,new Vector2Int(i,j));
        tiles[i,j]=tile;
        if ((i == 0 || j == 0) || (i == danceFloorSize - 1 || j == danceFloorSize - 1)) //check if tile is an extremity
        {
            invalidTileList.Add(tile);
            tile.thisState = TileState.Invalid;
        }
        
    }

    private void TeleportPLayerToCenterOfDanceFloor()
    {
        int halfSize = (int)Math.Floor((float)danceFloorSize / 2);
        Vector2Int coords = new Vector2Int(halfSize, halfSize);
        danceFloorManager.gameManager.DoMovePlayerTeleportToCoord(coords,danceFloorManager.associatedPLayerId);
        
    }


}
