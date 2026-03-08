using System;
using UnityEngine;

public class MovePlayer : Doer
{
    private PlayerManager playerManager;
    private TileId[,] allTileId = new TileId[,]{};
    private void Awake()
    {
        playerManager = (PlayerManager)manager;
    }

    private void Start()
    {
        GetAllUsefulParameters();
    }

    public override void GetAllUsefulParameters()
    {
        allTileId = playerManager.gameManager.GetAllTileid(playerManager.playerId);
    }

    public void TeleportPlayerToCoords(Vector2Int coords)
    {
        playerManager.currentPlayerCoord = coords;
        playerManager.currentTileId = allTileId[coords.x, coords.y];
        TileId currentTile = playerManager.currentTileId;
        playerManager.playerGameobject.transform.position=
            new Vector3(currentTile.transform.position.x,playerManager.playerGameobject.transform.position.y,currentTile.transform.position.z);

    }
}



