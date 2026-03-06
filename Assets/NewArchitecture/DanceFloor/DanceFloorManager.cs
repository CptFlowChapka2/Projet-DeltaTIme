using System;
using UnityEngine;

public class DanceFloorManager : Manager
{

    private CreateDanceFloor createDanceFloor;
   


    private void Awake()
    {
        GetAllDoersOnGameObject();
        InitializeDoer(out createDanceFloor);
    }
    
    
    public TileiD[,] allTileScript = new TileiD[,]{};
    public GameObject tilePrefab;
}
