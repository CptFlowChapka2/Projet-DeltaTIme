using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager
{
    private ReadInput readInput;
    private AnalyseInput analyseInput;
    private MovePlayer movePlayer;
    private UpdateWhenHit updateWhenHit;
    
    private void Awake()
    {
        GetAllDoersOnGameObject();
        InitializeDoer<ReadInput>(out readInput);
        InitializeDoer<AnalyseInput>(out analyseInput);
        InitializeDoer<MovePlayer>(out movePlayer);
        InitializeDoer<UpdateWhenHit>(out updateWhenHit);
        playerGameobject = gameObject;


    }

    [Header("GeneralInfo")] 
    public int playerId;
    public GameObject playerGameobject;
    public Vector2Int currentPlayerCoord=new Vector2Int();

    [Header("ReadInput")]
    public string inputActionName; // Player1Move ou Player2Move
    public Vector2Int inputThisFrame;

    [Header("AnalyseInput")] 
    public List<Vector2Int> inputsThisMeasure = new List<Vector2Int>();
    public int numberOfSuccesses = 0;

    [Header("MovePlayer")] 
    public TileId currentTileId;
    
    
    //OUTSIDE DO CALL//

    public void DoTeleportPlayerToCoords(Vector2Int coords)
    {
        movePlayer.TeleportPlayerToCoords(coords);
    }
    
}
