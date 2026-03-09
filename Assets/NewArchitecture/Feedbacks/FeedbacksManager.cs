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
    
    private void Start()
    {
        GetAllDoersOnGameObject();
        InitializeDoer(out movingInBeatFeedback);
    }

    public void Call(FeedbackCaller caller)
    {
        caller.Call();
    }
}
