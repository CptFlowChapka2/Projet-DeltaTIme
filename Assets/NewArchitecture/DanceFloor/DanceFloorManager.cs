using System;
using System.Collections.Generic;
using UnityEngine;

public class DanceFloorManager : Manager
{

    private CreateDanceFloor createDanceFloor;
    
    private void Awake()
    {
        GetAllDoersOnGameObject();
        InitializeDoer(out createDanceFloor);
    }

    public int associatedPlayerId;
    [Header("CreateDanceFloor")]
    public TileId[,] allTileID = new TileId[,]{};
    public List<TileId> allInvalideTile = new List<TileId>();
    public GameObject tilePrefab;
    public int danceFloorSize;

}
