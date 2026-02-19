using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputPerPlayer : MonoBehaviour
{
    public int playerNumber;
    public Vector2Int movement;
    private GameObject gm;
    private InputReader inputReader;

    private UnityEvent<Vector2Int> moveOrder = new UnityEvent<Vector2Int>();
    
    

    public int indexLastInputPlayed;

    private void Start()
    {
        gm = GameObject.Find("GM");
        moveOrder.AddListener(GetComponent<NewPlayerMovement>().ReceiveMoveOrder);
    }
    
    

    public void ReceiveInput(Vector2Int input,int id)
    {
        movement = Vector2Int.zero;
        if(id!=playerNumber)return;

        movement = input;
       
        moveOrder.Invoke(input);
        
    }
}
