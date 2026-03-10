using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : Doer
{
    private AttacksManager attacksManager;
    private List<ProjectileID> allActiveProjectiles = new List<ProjectileID>();
    private bool onBeatFlag;
    private void Awake()
    {
        attacksManager = (AttacksManager)manager;
    }

    public override void GetAllUsefulParameters()
    {
        onBeatFlag = attacksManager.gameManager.GetOnBeat();
       allActiveProjectiles =new List<ProjectileID>(attacksManager.currentlyActiveProjectileIds);
    }

    private void FixedUpdate()
    {
        attacksManager.ForceDoerToSetUsefullData<ManipulateProjectileList>();
        GetAllUsefulParameters();
        if (onBeatFlag)
        {
            
            allActiveProjectiles.ForEach(x =>
            {
               
                if (x.currentPlayerId == 0) goto SkipToRemove ;//le goto car la flemme
                TileId[,] allTile = attacksManager.gameManager.GetAllTileid(x.currentPlayerId);
                Vector2Int newCoords;
                if (CalculateNextTile(x, out newCoords,allTile))//est un retourn bool pour check si la case existe
                {
                    
                    MooveProjectileToTile(x,allTile[newCoords.x,newCoords.y]);
                    return;
                }
                SkipToRemove:
                x.PutOutOfUse();
                x.transform.position = attacksManager.inactiveProjectileIdPosition;
                attacksManager.gameManager.DoTileListModification
                    (new Vector2Int(),x,tileIdOrder.CurrentRemove,x.currentPlayerId,true);
                attacksManager.SwitchProjectileToInactiveList(x);
            });
        }
    }

    private bool CalculateNextTile(ProjectileID projectileID,out Vector2Int result,TileId[,] allTile)
    {
        
        Vector2Int potentialNewCoord = projectileID.currentRelativeCoords + projectileID.directionOfMouvement;
        Debug.Log(potentialNewCoord);
        result = Vector2Int.zero;
        if ((potentialNewCoord.x < allTile.GetLowerBound(0) || potentialNewCoord.x > allTile.GetUpperBound(0)) ||
            (potentialNewCoord.y < allTile.GetLowerBound(1) || potentialNewCoord.y > allTile.GetUpperBound(1)))
            return false;
        result = potentialNewCoord;
        return true;
    }

    private void MooveProjectileToTile(ProjectileID projectileID,TileId nextTile)
    {
        projectileID.transform.position = nextTile.gameObject.transform.position;

       Vector2Int currentRelativeCoords = projectileID.currentRelativeCoords;
       int currentPlayerId = projectileID.currentPlayerId;
        
       attacksManager.gameManager.DoTileListModification(currentRelativeCoords,projectileID,tileIdOrder.CurrentRemove,currentPlayerId);
       attacksManager.gameManager.DoTileListModification(currentRelativeCoords,projectileID,tileIdOrder.SignRemove,currentPlayerId);
        
       projectileID.currentTileId = nextTile;
       projectileID.currentRelativeCoords = projectileID.currentTileId.position;
        
       attacksManager.gameManager.DoTileListModification(currentRelativeCoords,projectileID,tileIdOrder.CurrentAdd,currentPlayerId);
       attacksManager.gameManager.DoTileListModification(currentRelativeCoords+projectileID.directionOfMouvement,projectileID,tileIdOrder.SignAdd,currentPlayerId);
    }
}
