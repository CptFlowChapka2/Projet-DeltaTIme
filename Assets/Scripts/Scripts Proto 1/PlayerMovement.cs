using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridParameters gridParameters;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private BeatEnabler beatEnabler;
    [SerializeField] private int numberOfBeatsBeforeRecovery;
    private int gridSize;
    public bool alreadyMovedThisBeat;
    public bool hasMadeAnError;
    public int beatsSinceError;

    private void Start()
    {
        gridSize = (int)gridParameters.gridSize;
        int startX = Random.Range(0, gridSize);
        int startZ = Random.Range(0, gridSize);
        transform.position = new Vector3(startX, transform.position.y, startZ);
    }

    private void Update()
    {
        if (!alreadyMovedThisBeat && !hasMadeAnError)
        {
            // MoveOnGrid   ();
            ClampingOnGrid();
        }

        if (beatsSinceError == numberOfBeatsBeforeRecovery)
        {
            hasMadeAnError = false;
            beatsSinceError = 0;
        }
    }

    // private void MoveOnGrid()
    // {
    //     //Au moment d'appuyer sur une direction...
    //     if (inputReader.HorizontalMoveP1 != 0 || inputReader.VerticalMoveP1 != 0)
    //     {
    //         //...On fait en sorte que cette direction soit la seule enregistrée avant le prochain beat...
    //         alreadyMovedThisBeat = true;
    //         //... et si c'est hors beat, on appelle une erreur.
    //         // if (!beatEnabler.onBeat)
    //         // {
    //         //     hasMadeAnError = true;
    //         // }
    //     }
    //     Vector3 pos = transform.position;
    //     pos = new Vector3(pos.x + inputReader.HorizontalMoveP1, pos.y, pos.z + inputReader.VerticalMoveP1);
    //     transform.position = pos;
    // }

    private void ClampingOnGrid()
    {
        if (transform.position.x >= gridParameters.gridSize)
        {
            transform.position = new Vector3(gridParameters.gridSize - 1, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }

        if (transform.position.z >= gridParameters.gridSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, gridParameters.gridSize - 1);
        }
        else if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
