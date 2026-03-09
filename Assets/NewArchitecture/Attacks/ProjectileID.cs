using UnityEngine;

public class ProjectileID : MonoBehaviour
{
    public AttacksManager attacksManager;
    
    public TileId currentTileId;
    public Vector2Int currentRelativeCoords;
    public Vector2Int directionOfMouvement;
    public int currentPlayerId;

    public void PutInUse(Vector2Int dir,TileId firstTile,int playerId)
    {
        directionOfMouvement = dir;
        currentTileId = firstTile;
        currentPlayerId = playerId;
        currentRelativeCoords = currentTileId.position;
        attacksManager.gameManager.DoTileListModification(currentTileId.position,this,tileIdOrder.CurrentAdd,currentPlayerId);
        attacksManager.gameManager.DoTileListModification(currentTileId.position+directionOfMouvement,this,tileIdOrder.SignAdd,currentPlayerId);

        
    }

    public void PutOutOfUse()
    {
        directionOfMouvement = new Vector2Int();
        currentTileId = null;
        currentPlayerId = 0;
        currentRelativeCoords = new Vector2Int();
    }
    
    
}
