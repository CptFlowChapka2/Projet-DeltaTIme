using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputAction player1Move;
    private InputAction player2Move;
    private UnityEvent<Vector2,int> playerInput = new UnityEvent<Vector2, int>();


    private void Start()
    {
        AddEventListener();
        player1Move = InputSystem.actions.FindAction("Player1Move");
        player2Move = InputSystem.actions.FindAction("Player2Move");
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
        
        playerInput.Invoke(playerRead,playerID);
        
    }

    private void AddEventListener()
    {
        FindObjectsByType<InputPerPlayer>(FindObjectsInactive.Exclude,FindObjectsSortMode.InstanceID).ToList().
            ForEach(x=>playerInput.AddListener(x.ReceiveInput));
    }
}
