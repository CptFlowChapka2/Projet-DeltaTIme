using System;
using System.Collections.Generic;
using System.Linq;
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
    public UpdatingCrowd updatingCrowd;
    
    
    private void Start()
    {
        GetAllDoersOnGameObject();
        InitializeDoer(out movingInBeatFeedback);
        updatingAnnouncersP1.Manager = this;
        updatingAnnouncersP2.Manager = this;
        InitializeDoer(out takingHitFeedback);
        
    }

    public void Call(FeedbackCaller caller)
    {
        caller.Call();
    }

    public void Calle<Tfc>(int playerId=0) where Tfc : FeedbackCaller
    {
        if (playerId != 0)
        {
            ((Tfc)allDoers.Where(x => x.GetType() == typeof(Tfc)).ToList().First()).Call();
            return;
        }

        foreach (var doer1 in allDoers.Where(x => x.GetType() == typeof(Tfc)).ToList())
        {
            var doer = (Tfc)doer1;
            doer.Call();
        }
    }
}
