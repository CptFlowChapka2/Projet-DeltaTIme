using System;
using UnityEngine;

public class InputPerPlayer : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    public Vector2 movement;
    private GameObject gm;
    private InputReader inputReader;

    public int numberOfLeftThisMeasure = 0;
    public int numberOfRightThisMeasure = 0;
    public int numberOfUpThisMeasure = 0;
    public int numberOfDownThisMeasure = 0;

    public int indexLastInputPlayed;

    private void Start()
    {
        gm = GameObject.Find("GM");
        inputReader = gm.GetComponent<InputReader>();
    }

    private void Update()
    {
        movement = Vector2.zero;
        SplittingInputsBeetweenPlayers();
        CountingEveryIterationsOfMovements();
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

    private void SplittingInputsBeetweenPlayers()
    {
        if (playerNumber == 1)
        {
            movement = new Vector2(inputReader.HorizontalMoveP1, inputReader.VerticalMoveP1);
        }
        else if (playerNumber == 2)
        {
            movement = new Vector2(inputReader.HorizontalMoveP2, inputReader.VerticalMoveP2);
        }
    }
}
