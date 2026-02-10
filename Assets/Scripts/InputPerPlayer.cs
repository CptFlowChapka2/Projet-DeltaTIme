using System;
using UnityEngine;

public class InputPerPlayer : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    public Vector2 movement;
    private GameObject gm;
    private InputReader inputReader;

    private void Start()
    {
        gm = GameObject.Find("GM");
        inputReader = gm.GetComponent<InputReader>();
    }

    private void Update()
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
