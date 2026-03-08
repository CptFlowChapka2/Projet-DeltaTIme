using System;
using UnityEngine;

public class MovePlayer : Doer
{
    private PlayerManager playerManager;
    private TileId[,] allTileId = new TileId[,]{};
    private Vector2Int currentPlayerCoords;
    private TileId currentTileId;
    private GameObject playerGameObject;
    
    
    
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
        currentPlayerCoords = playerManager.currentPlayerCoords;
        currentTileId = playerManager.currentTileId;
        playerGameObject = playerManager.playerGameObject;
    }

    public override void SetAllUsedParameters()
    {
        playerManager.currentPlayerCoords = currentPlayerCoords;
        playerManager.currentTileId = currentTileId;
        playerManager.playerGameObject = playerGameObject;
    }

    public void TeleportPlayerToCoords(Vector2Int coords)
    {
        currentPlayerCoords = coords;
        currentTileId = allTileId[coords.x, coords.y];
        TileId currentTile = currentTileId;
        playerGameObject.transform.position=
            new Vector3(currentTile.transform.position.x,playerGameObject.transform.position.y,currentTile.transform.position.z);
        SetAllUsedParameters();
    }
}



