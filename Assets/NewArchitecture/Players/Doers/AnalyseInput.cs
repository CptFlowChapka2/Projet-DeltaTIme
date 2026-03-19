using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnalyseInput : Doer
{
    private PlayerManager playerManager;

    private Vector2Int inputThisFrame;
    private bool inCoyoteBeat;
    //private bool onEndCoyoteBeat;
    //private bool onStartCoyoteMeasure;
    private int numberOfSuccesses = 0;
    
    private Vector2Int[] inputsThisMeasureCache =  new Vector2Int[4];
    private int currentBeat = 0;

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
        SetAllUsedParameters();
    }

    public override void GetAllUsefulParameters()
    {
        inCoyoteBeat = playerManager.gameManager.GetInCoyoteBeat();
        //onEndCoyoteBeat = playerManager.gameManager.GetOnEndCoyoteBeat(); 
        //onStartCoyoteMeasure = playerManager.gameManager.GetOnStartCoyoteMeasure();
        inputThisFrame = playerManager.inputThisFrame;
        inputsThisMeasureCache = playerManager.inputsThisMeasureCache;
    }

    public override void SetAllUsedParameters()
    {
        playerManager.inputsThisMeasureCache = inputsThisMeasureCache;
    }

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
        inputsThisMeasureCache[currentBeat] = inputThisFrame;
        alreadyInputedThisBeat = true;
    }

    public void ListenForOnEndCoyoteBeat()
    {
        foreach (var i in inputsThisMeasureCache)
        {
            Debug.Log(i);
        }
        
        if (alreadyInputedThisBeat == false)
        {
            inputsThisMeasureCache[currentBeat] = Vector2Int.zero;
        }
        alreadyInputedThisBeat = false;
        currentBeat++;
    }

    public void ListenForOnBeginningMeasure()
    {
        playerManager.numberOfSuccesses = numberOfSuccesses;
        numberOfSuccesses = 0;
        currentBeat = 0;
        playerManager.inputsThisMeasure = inputsThisMeasureCache.ToList();
        for (int i = 0; i < inputsThisMeasureCache.Length; i++)
        {
            inputsThisMeasureCache[i] = new Vector2Int(-1, -1);
        }
    }
    
    private void ResolveDiagonals(Vector2Int input, out Vector2Int rInput)
    {
        rInput = new Vector2Int(input.x, 0);
    }
}
