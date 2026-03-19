using System;
using System.Collections.Generic;
using UnityEngine;

public class CreateDanceFloor : Doer
{
    private DanceFloorManager danceFloorManager;
    private TileId[,] allTileId = new TileId[,]{};
    private List<TileId> allInvalideTile = new List<TileId>();
    private GameObject tilePrefab;
    private int danceFloorSize;

    private void Awake()
    {
        danceFloorManager = (DanceFloorManager)manager;
    }

    private void Start()
    {
        GetAllUsefulParameters();
        allTileId= InitialiseDanceFloorArray();
        FindDanceFloor();
        SetAllUsedParameters();
        danceFloorManager.gameManager.DoCallAllManagerOfTypeToForceGetUsefullData<PlayerManager,MovePlayer>();
        TeleportPLayerToCenterOfDanceFloor();
    }

    public override void GetAllUsefulParameters()
    {
        tilePrefab = danceFloorManager.tilePrefab;
        danceFloorSize = danceFloorManager.danceFloorSize + 2;
    }

    public override void SetAllUsedParameters()
    {
        danceFloorManager.allTileID = allTileId;
        danceFloorManager.allInvalideTile = allInvalideTile;
    }

    private TileId[,]  InitialiseDanceFloorArray()
    {
        return new TileId[danceFloorSize, danceFloorSize];
    }
    
    private void FindDanceFloor()
    {
        TileId[] unsortedTiles = GetComponentsInChildren<TileId>();

        foreach (TileId tile in unsortedTiles)
        {
            allTileId[tile.position.x, tile.position.y] = tile;
            tile.danceFloorManager = danceFloorManager;
            if ((tile.position.x == 0 || tile.position.y == 0) || (tile.position.x == danceFloorSize - 1 || tile.position.y == danceFloorSize - 1)) //check if tile is an extremity
            {
                allInvalideTile.Add(tile);
                tile.thisState = TileState.Invalid;
            }
            tile.gameObject.GetComponent<UpdateTileMaterial>().Initialize();
        }
    }
    // private void CreateTile(Vector3 pos, int i, int j, TileId[,] tiles,List< TileId> invalidTileList)
    // {
    //     TileId tile = Instantiate(tilePrefab, pos, Quaternion.identity).GetComponent<TileId>();
    //     tile.Initialise(danceFloorManager,new Vector2Int(i,j));
    //     tiles[i,j]=tile;
    //     if ((i == 0 || j == 0) || (i == danceFloorSize - 1 || j == danceFloorSize - 1)) //check if tile is an extremity
    //     {
    //         invalidTileList.Add(tile);
    //         tile.thisState = TileState.Invalid;
    //     }
    // }

    private void TeleportPLayerToCenterOfDanceFloor()
    {
        int halfSize = (int)Math.Floor((float)danceFloorSize / 2);
        Vector2Int coords = new Vector2Int(halfSize, halfSize);
        Debug.Log("coord to tp to center "+coords+"player "+danceFloorManager.associatedPlayerId);
            danceFloorManager.gameManager.DoMovePlayerTeleportToCoord(coords,danceFloorManager.associatedPlayerId);
    }


}
