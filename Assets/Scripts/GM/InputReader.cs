using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputAction player1Move;
    private InputAction player2Move;
    private UnityEvent<Vector2Int,int> playerInput = new UnityEvent<Vector2Int, int>();


    private void Start()
    {
        
        player1Move = InputSystem.actions.FindAction("Player1Move");
        player2Move = InputSystem.actions.FindAction("Player2Move");
        AddEventListener();
    }

    private void Update()
    {
        CheckPlayerInputs(player1Move,1);
        CheckPlayerInputs(player2Move,2);
    }

    private void  CheckPlayerInputs(InputAction playerXMove,int playerID)
    {
        if(!playerXMove.WasPerformedThisFrame())return;
        Vector2 playerRead = playerXMove.ReadValue<Vector2>();
        Vector2Int playerSend = new Vector2Int((int)Math.Floor(playerRead.x),(int)Math.Floor(playerRead.y));
        playerInput.Invoke(playerSend,playerID);
        
    }

    private void AddEventListener()
    {
        FindObjectsByType<InputPerPlayer>(FindObjectsInactive.Exclude,FindObjectsSortMode.InstanceID).ToList().
            ForEach(x=>playerInput.AddListener(x.ReceiveInput));
    }
}
