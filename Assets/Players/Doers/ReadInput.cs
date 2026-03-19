using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadInput : Doer
{
    private PlayerManager playerManager;
    
    private InputAction playerMove;

    private void Awake()
    {
        playerManager = (PlayerManager)manager;
    }

    private void Start()
    {
        
        playerMove = InputSystem.actions.FindAction(playerManager.inputActionName);
    }

    private void Update()//todo : remettre fixUpdate
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
