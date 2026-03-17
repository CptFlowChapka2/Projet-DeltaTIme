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


    public void DoModifyTileList(Vector2Int tileToModify, ProjectileID toDo, tileIdOrder thingToDo,bool overwriteToAll=false)
    {
        switch (thingToDo)
        {
            case tileIdOrder.CurrentAdd:
                if(overwriteToAll)
                    foreach (var tileId in allTileID)
                    {
                        tileId.AddAProjectileOnThisTile(toDo);
                        return;
                    }

                allTileID[tileToModify.x,tileToModify.y].AddAProjectileOnThisTile(toDo);
                break;
            case tileIdOrder.CurrentRemove:
                if(overwriteToAll)
                    foreach (var tileId in allTileID)
                    {
                        tileId.RemoveAProjectileOnThisTile(toDo);
                        return;
                    }
                allTileID[tileToModify.x,tileToModify.y].RemoveAProjectileOnThisTile(toDo);
                break;
            case tileIdOrder.SignAdd:
                if(overwriteToAll)
                    foreach (var tileId in allTileID)
                    {
                        tileId.AddASignOnThisTile(toDo);
                        return;
                    }
                allTileID[tileToModify.x,tileToModify.y].AddASignOnThisTile(toDo);
                break;
            case tileIdOrder.SignRemove:
                if(overwriteToAll)
                    foreach (var tileId in allTileID)
                    {
                        tileId.RemoveASignOnThisTile(toDo);
                        return;
                    }
                allTileID[tileToModify.x,tileToModify.y].RemoveASignOnThisTile(toDo);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(thingToDo), thingToDo, null);
        }
    }
    
    
}
