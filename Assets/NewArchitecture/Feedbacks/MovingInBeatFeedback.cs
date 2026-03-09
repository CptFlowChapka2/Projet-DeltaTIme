using System;
using FMODUnity;
using UnityEngine;

public class MovingInBeatFeedback : FeedbackCaller
{
    private FeedbacksManager feedbacksManager;
    
    [SerializeField] private StudioEventEmitter movingInBeatFeedbackEmitter;

    private void Start()
    {
        feedbacksManager = (FeedbacksManager)manager;
    }

    public override void Call()
    {
        movingInBeatFeedbackEmitter.Play();
    }
}
