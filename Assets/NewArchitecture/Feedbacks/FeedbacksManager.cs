using System;
using FMODUnity;
using UnityEngine;

public enum PlayerState
{
    Safe,
    Damaged,
    Undefined
}

public class FeedbacksManager : Manager
{
    public MovingInBeatFeedback movingInBeatFeedback;
    public TakingHitFeedback takingHitFeedback;
    
    private void Start()
    {
        GetAllDoersOnGameObject();
        InitializeDoer(out movingInBeatFeedback);
        InitializeDoer(out takingHitFeedback);
    }

    public void Call(FeedbackCaller caller)
    {
        caller.Call();
    }
}
