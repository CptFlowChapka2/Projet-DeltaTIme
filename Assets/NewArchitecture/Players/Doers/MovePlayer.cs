using UnityEngine;

public class MovePlayer : Doer
{
    private PlayerManager playerManager;
    private TileiD[,] allTileId = new TileiD[,]{};
    private void Start()
    {
        playerManager = (PlayerManager)manager;
    }

    protected override void GetAllUsefulParameters()
    {
        allTileId = playerManager.gameManager.GetAllTileid(playerManager.playerId);
    }

    public void TeleportPlayerToCoords(Vector2Int coords)
    {
        playerManager.currentPlayerCoord = coords;
        playerManager.currentTileId = allTileId[coords.x, coords.y];
        TileiD currentTile = playerManager.currentTileId;
        playerManager.playerGameobject.transform.position=
            new Vector3(currentTile.transform.position.x,playerManager.playerGameobject.transform.position.y,currentTile.transform.position.z);

    }
}



