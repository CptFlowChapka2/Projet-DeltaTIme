using FMODUnity;
using UnityEngine;

public class TakingHitFeedback : FeedbackCaller
{
    private FeedbacksManager feedbacksManager;
    
    [SerializeField] private StudioEventEmitter takingHitFeedbackEmitter;

    private void Start()
    {
        feedbacksManager = (FeedbacksManager)manager;
    }
    
    public override void Call()
    {
        takingHitFeedbackEmitter.Play();
    }
}
