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

    public int associatedPLayerId;
    [Header("CreateDanceFloor")]
    public TileiD[,] allTileID = new TileiD[,]{};
    public List<NewTileScript> allInvalideTile = new List<NewTileScript>();
    public GameObject tilePrefab;
    public int danceFloorSize;

}
