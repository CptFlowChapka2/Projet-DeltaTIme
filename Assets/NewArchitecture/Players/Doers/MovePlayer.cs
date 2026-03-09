using System;
using UnityEngine;

public class MovePlayer : Doer
{
    private PlayerManager playerManager;
    
    private TileId[,] allTileId = new TileId[,]{};
    private Vector2Int currentPlayerCoords;
    private TileId currentTileId;
    private GameObject playerGameObject;
    private Vector2Int inputThisFrame;
    private int danceFloorSize;
    
    
    private void Awake()
    {
        playerManager = (PlayerManager)manager;
    }

    private void Start()
    {
        GetAllUsefulParameters();
    }

    private void Update()
    {
        GetAllUsefulParameters();
        if (inputThisFrame != Vector2Int.zero)
        {
            MovePlayerWithInputs();
        }
    }

    public override void GetAllUsefulParameters()
    {
        allTileId = playerManager.gameManager.GetAllTileid(playerManager.playerId);
        danceFloorSize = playerManager.gameManager.GetDanceFloorSize();
        currentPlayerCoords = playerManager.currentPlayerCoords;
        currentTileId = playerManager.currentTileId;
        playerGameObject = playerManager.playerGameObject;
        inputThisFrame = playerManager.inputThisFrame;
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
        currentPlayerCoords = new Vector2Int(Mathf.Clamp(currentPlayerCoords.x, 0, danceFloorSize + 1), Mathf.Clamp(currentPlayerCoords.y, 0, danceFloorSize + 1));
        currentTileId = allTileId[currentPlayerCoords.x, currentPlayerCoords.y];
        TileId currentTile = currentTileId;
        playerGameObject.transform.position=
            new Vector3(currentTile.transform.position.x,playerGameObject.transform.position.y,currentTile.transform.position.z);
        SetAllUsedParameters();
    }

    public void MovePlayerWithInputs()
    {
        TeleportPlayerToCoords(currentPlayerCoords + inputThisFrame);
    }
}



