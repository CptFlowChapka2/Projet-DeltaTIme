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
    private UnityEvent<int,Vector2Int> playerMooved=new UnityEvent<int,Vector2Int>();
    public Dictionary<Vector2Int, NewTileScript> tiles = new Dictionary<Vector2Int, NewTileScript>();

    public List<KeyValuePair<Vector2Int, NewTileScript>> invalideTile =
        new List<KeyValuePair<Vector2Int, NewTileScript>>();
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
        if (!tiles.ContainsKey(currentTile.position+input)
            &&tiles[currentTile.position+input].thisState!=NewTileScript.TileState.Invalid)return;
        currentTile = tiles[currentTile.position + input];

        transform.position = new Vector3(currentTile.transform.position.x, transform.position.y,
            currentTile.transform.position.z);
        
        playerMooved.Invoke(inputPerPlayer.playerNumber,input);
    }
    

    private void InitialisedLocalEvents()
    {
        playerMooved.AddListener(FindAnyObjectByType<EventSyncroniser>().ReceivePlayerMoove);
    }
}
