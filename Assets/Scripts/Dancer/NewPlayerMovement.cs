using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class NewPlayerMovement : MonoBehaviour
{
    private GameObject gm;
    private NewDanceFloorSpawner _danceFloorSpawner;
    private InputPerPlayer inputPerPlayer;
    public NewTileScript currentTile;
    private UnityEvent<int,Vector2Int> playerMoved = new UnityEvent<int,Vector2Int>();
    public NewTileScript[,] thatPLayerGrid = new NewTileScript[,]{};

    public List< NewTileScript> invalideTile = new List<NewTileScript>();
    private int gridSize;
    public int thisPLayer;

    private void Start()
    {
        gm = GameObject.Find("GM");
        _danceFloorSpawner = gm.GetComponent<NewDanceFloorSpawner>();
        inputPerPlayer = GetComponent<InputPerPlayer>();
        InitialisedLocalEvents();
    }
    

    public void ReceiveMoveOrder(Vector2Int input)
    {
        MoveOnGrid(input);
    }

    private void MoveOnGrid(Vector2Int input)
    {
        Vector2Int newPositionCoord = currentTile.position + input;
        bool newPositionExist = newPositionCoord.x  <= thatPLayerGrid.GetUpperBound(0) &&
                                newPositionCoord.y <= thatPLayerGrid.GetUpperBound(1)&&
                                newPositionCoord.x  >= thatPLayerGrid.GetLowerBound(0) &&
                                newPositionCoord.y >= thatPLayerGrid.GetLowerBound(1)
            ;
        if (!newPositionExist) return;
        NewTileScript nextPosition = thatPLayerGrid[newPositionCoord.x, newPositionCoord.y];
        

        if (nextPosition.thisState==NewTileScript.TileState.Invalid)return;
        currentTile = nextPosition;
        transform.position = new Vector3(currentTile.transform.position.x, transform.position.y,
            currentTile.transform.position.z);
        
        playerMoved.Invoke(inputPerPlayer.playerNumber,input);
    }
    

    private void InitialisedLocalEvents()
    {
        playerMoved.AddListener(FindAnyObjectByType<EventSyncroniser>().ReceivePlayerMove);
    }
}
