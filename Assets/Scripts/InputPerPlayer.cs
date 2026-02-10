using System;
using UnityEngine;

public class InputPerPlayer : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    public Vector2 movement;
    private GameObject gm;
    private InputReader inputReader;

    public int numberOfLeftInputsThisMeasure = 0;
    public int numberOfRightInputsThisMeasure = 0;
    public int numberOfUpInputsThisMeasure = 0;
    public int numberOfDownInputsThisMeasure = 0;

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
            numberOfLeftInputsThisMeasure++;
        }
        else if (movement.x > 0)
        {
            numberOfRightInputsThisMeasure++;
        }

        if (movement.y < 0)
        {
            numberOfDownInputsThisMeasure++;
        }
        else if (movement.y > 0)
        { 
            numberOfUpInputsThisMeasure++;
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
