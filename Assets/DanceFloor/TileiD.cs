using System;
using System.Collections.Generic;
using UnityEngine;

public class TileId : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);
    public TileState thisState = TileState.Safe;
    public List<ProjectileID> allProjectileOnThisTile = new List<ProjectileID>();
    public List<ProjectileID> allProjectileOnThisTileNextBeat = new List<ProjectileID>();

    public DanceFloorManager danceFloorManager;

    // public void Initialise(DanceFloorManager iniDanceFloorManager,Vector2Int iniPosition)
    // {
    //     danceFloorManager = iniDanceFloorManager;
    //     position = iniPosition;
    // }

    public void AddAProjectileOnThisTile(ProjectileID projectileID)
    {
        allProjectileOnThisTile.Add(projectileID);
    }
    public void RemoveAProjectileOnThisTile(ProjectileID projectileID)
    {
        allProjectileOnThisTile.RemoveAll(x=>projectileID);
    }
    public void AddASignOnThisTile(ProjectileID projectileID)
    {
        allProjectileOnThisTileNextBeat.Add(projectileID);
    }
    public void RemoveASignOnThisTile(ProjectileID projectileID)
    {
        allProjectileOnThisTileNextBeat.RemoveAll(x=>projectileID);
    }
}
