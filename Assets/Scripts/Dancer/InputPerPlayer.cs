using System;
using UnityEngine;
using UnityEngine.Events;

public class InputPerPlayer : MonoBehaviour
{
    public int playerNumber;
    public Vector2 movement;
    private GameObject gm;
    private InputReader inputReader;

    private UnityEvent moveOrder = new UnityEvent();

    public int numberOfLeftThisMeasure = 0;
    public int numberOfRightThisMeasure = 0;
    public int numberOfUpThisMeasure = 0;
    public int numberOfDownThisMeasure = 0;

    public int indexLastInputPlayed;

    private void Start()
    {
        gm = GameObject.Find("GM");
        moveOrder.AddListener(GetComponent<NewPlayerMovement>().ReceiveMoveOrder);
    }
    
    private void CountingEveryIterationsOfMovements()
    {
        if (movement.x < 0)
        {
            numberOfLeftThisMeasure++;
            indexLastInputPlayed = 0;
        }
        else if (movement.x > 0)
        {
            numberOfRightThisMeasure++;
            indexLastInputPlayed = 1;
        }

        if (movement.y < 0)
        {
            numberOfDownThisMeasure++;
            indexLastInputPlayed = 3;
        }
        else if (movement.y > 0)
        { 
            numberOfUpThisMeasure++;
            indexLastInputPlayed = 2;
        }
    }

    public void ReceiveInput(Vector2 input,int id)
    {
        movement = Vector2.zero;
        if(id!=playerNumber)return;

        movement = input;
        CountingEveryIterationsOfMovements();
        moveOrder.Invoke();
        
    }
}
