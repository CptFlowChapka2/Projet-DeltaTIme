using System;
using UnityEngine;

public class MoveProjectile : Doer
{
    private AttacksManager attacksManager;
    private bool onBeatFlag;
    private void Awake()
    {
        attacksManager = (AttacksManager)manager;
    }

    public override void GetAllUsefulParameters()
    {
        onBeatFlag = attacksManager.gameManager.GetInCoyoteBeat();
    }

    private void FixedUpdate()
    {
        GetAllUsefulParameters();
        if (onBeatFlag)
        {
            attacksManager.currentlyActiveProjectileIds.ForEach(x =>
            {
                TileId[,] allTile = attacksManager.gameManager.GetAllTileid(x.currentPlayerId);
                Vector2Int newCoords;
                if (CalculateNextTile(x, out newCoords,allTile))//est un retourn bool pour check si la case existe
                {
                    
                    MooveProjectileToTile(x,allTile[newCoords.x,newCoords.y]);
                }
                else// si la case n'exsite pas le projectile est désafecté.
                {
                    x.PutOutOfUse();
                    attacksManager.gameManager.DoTileListModification
                        (new Vector2Int(),x,tileIdOrder.CurrentRemove,x.currentPlayerId,true);
                    attacksManager.currentlyActiveProjectileIds.Remove(x);
                }
            });
        }
    }

    private bool CalculateNextTile(ProjectileID projectileID,out Vector2Int result,TileId[,] allTile)
    {
        
        Vector2Int potentialNewCoord = projectileID.currentRelativeCoords + projectileID.directionOfMouvement;
        result = Vector2Int.zero;
        if ((potentialNewCoord.x < allTile.GetLowerBound(0) && potentialNewCoord.x > allTile.GetUpperBound(0)) ||
            (potentialNewCoord.y < allTile.GetLowerBound(1) && potentialNewCoord.y > allTile.GetUpperBound(1)))
            return false;
        result = potentialNewCoord;
        return true;
    }

    private void MooveProjectileToTile(ProjectileID projectileID,TileId nextTile)
    {
        transform.position = nextTile.gameObject.transform.position;

       Vector2Int currentRelativeCoords = projectileID.currentRelativeCoords;
       int currentPlayerId = projectileID.currentPlayerId;
        attacksManager.gameManager.DoTileListModification(currentRelativeCoords,projectileID,tileIdOrder.CurrentRemove,currentPlayerId);
        projectileID.currentTileId = nextTile;
        projectileID.currentRelativeCoords = projectileID.currentTileId.position;
        attacksManager.gameManager.DoTileListModification(currentRelativeCoords,projectileID,tileIdOrder.CurrentAdd,currentPlayerId);
        attacksManager.gameManager.DoTileListModification(currentRelativeCoords,projectileID,tileIdOrder.SignRemove,currentPlayerId);
        attacksManager.gameManager.DoTileListModification(currentRelativeCoords+projectileID.directionOfMouvement,projectileID,tileIdOrder.SignAdd,currentPlayerId);
    }
}
