using System;
using System.Collections.Generic;
using UnityEngine;

public class TileiD : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);
    public TileState thisState = TileState.Safe;
    public List<ProjectileID> allProjectileOnThisTile = new List<ProjectileID>();
    public List<ProjectileID> allProjectileOnThisTileNextBeat = new List<ProjectileID>();

    public DanceFloorManager danceFloorManager;

    public void Initialise(DanceFloorManager iniDanceFloorManager,Vector2Int iniPosition)
    {
        danceFloorManager = iniDanceFloorManager;
        position = iniPosition;
    }
    
    
}
