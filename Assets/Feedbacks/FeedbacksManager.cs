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
    public UpdatingAnnouncers updatingAnnouncersP1;
    public UpdatingAnnouncers updatingAnnouncersP2;
    
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
