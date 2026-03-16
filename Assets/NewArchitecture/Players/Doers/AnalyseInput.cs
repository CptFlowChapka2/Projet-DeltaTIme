using System;
using System.Collections.Generic;
using UnityEngine;

public class AnalyseInput : Doer
{
    private PlayerManager playerManager;

    private Vector2Int inputThisFrame;
    private bool inCoyoteBeat;
    //private bool onEndCoyoteBeat;
    //private bool onStartCoyoteMeasure;
    private int numberOfSuccesses = 0;

    private void Awake()
    {
        playerManager = (PlayerManager)manager;
    }

    private void Update()
    {
        GetAllUsefulParameters();
        //ListenForOnEndCoyoteBeat();
        CreateInputsThisMeasure();
        //ListenForOnStartCoyoteMeasure();
    }

    public override void GetAllUsefulParameters()
    {
        inCoyoteBeat = playerManager.gameManager.GetInCoyoteBeat();
        //onEndCoyoteBeat = playerManager.gameManager.GetOnEndCoyoteBeat(); 
        //onStartCoyoteMeasure = playerManager.gameManager.GetOnStartCoyoteMeasure();
        inputThisFrame = playerManager.inputThisFrame;
    }

    private List<Vector2Int> inputsThisMeasureCache = new List<Vector2Int>();
    private bool alreadyInputedThisBeat = false;
    
    private void CreateInputsThisMeasure()
    {
        if (inputThisFrame == Vector2Int.zero || alreadyInputedThisBeat) return;
        if (inputThisFrame.x != 0 && inputThisFrame.y != 0) ResolveDiagonals(inputThisFrame, out inputThisFrame);
        if (!inCoyoteBeat)
        {
            inputThisFrame = Vector2Int.zero;
            //Debug.Log("Oops");
            //todo call feedback
        }
        else
        {
            playerManager.gameManager.DoCallSpecificFeedback(playerManager.gameManager.feedbacksManager.movingInBeatFeedback);
            numberOfSuccesses++;
        }
        inputsThisMeasureCache.Add(inputThisFrame);
        alreadyInputedThisBeat = true;
    }

    public void ListenForOnEndCoyoteBeat()
    {
        alreadyInputedThisBeat = false;
    }

    public void ListenForOnStartCoyoteMeasure()
    {
        playerManager.numberOfSuccesses = numberOfSuccesses;
        //Debug.Log(numberOfSuccesses);
        numberOfSuccesses = 0;
        playerManager.inputsThisMeasure = inputsThisMeasureCache;
        inputsThisMeasureCache.Clear();
    }
    
    private void ResolveDiagonals(Vector2Int input, out Vector2Int rInput)
    {
        rInput = new Vector2Int(input.x, 0);
    }
}
