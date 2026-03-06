using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadInput : Doer
{
    private PlayerManager playerManager;
    
    private InputAction playerMove;

    private void Start()
    {
        playerManager = (PlayerManager)manager;
        playerMove = InputSystem.actions.FindAction(playerManager.inputActionName);
    }

    private void FixedUpdate()
    {
        playerManager.inputThisFrame = GetPlayerInputThisFrame();
    }

    private Vector2Int GetPlayerInputThisFrame()
    {
        if(!playerMove.WasPerformedThisFrame())return Vector2Int.zero;
        
        Vector2 playerRead = playerMove.ReadValue<Vector2>();
        Vector2Int playerSend = new Vector2Int((int)Math.Floor(playerRead.x),(int)Math.Floor(playerRead.y));
        
        return playerSend;
    }
}
