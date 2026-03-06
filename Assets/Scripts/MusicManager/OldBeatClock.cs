using System;
using UnityEngine;
using UnityEngine.Events;

public class OldBeatClock : MonoBehaviour
{
    public int beat = 1;
    public int measure = 1;
    [SerializeField] private float playerCoyoteBeat=0.25f;
    public UnityEvent startCoyoteTime = new UnityEvent();
    public UnityEvent onBeat = new UnityEvent();
    public UnityEvent onEndBeat = new UnityEvent();
    public UnityEvent endCoyoteTime = new UnityEvent();
    public UnityEvent startCoyoteBeforeMeasure = new UnityEvent();
    public UnityEvent onMeasure = new UnityEvent();
    public UnityEvent endCoyoteAfterMeasure = new UnityEvent();
    private MasterClock masterClock;
    private MusicParametersForFMOD musicParameters;
    private int currentBPM;
    private double timePerBeat;

    private bool inBeatFlag = false;
    private bool inMesureFlag = false;

    private void Start()
    {
        masterClock = GetComponent<MasterClock>();
        musicParameters = GetComponent<MusicParametersForFMOD>();
        currentBPM = musicParameters.beatsPerMinute;
        timePerBeat = 60.0 / currentBPM;
        EventSyncroniser eventSyncroniser = FindAnyObjectByType<EventSyncroniser>();
        startCoyoteTime.AddListener(eventSyncroniser.ReceiveStartCoyote);
        endCoyoteTime.AddListener(eventSyncroniser.ReceiveEndCoyote);
    }

    // private void FixedUpdate()
    // {
    //     int numberOfPreviousBeats = beat - 1 + (musicParameters.beatsPerMeasure * (measure - 1));
    //     double timeSinceLastBeat = masterClock.timeInSeconds - numberOfPreviousBeats * timePerBeat;
    //
    //     if (masterClock.timeInSeconds < timePerBeat)
    //     {
    //         beat = 1;
    //         measure = 1;
    //     }
    //     
    //     if (inBeatFlag && timeSinceLastBeat >= playerCoyoteBeat && timeSinceLastBeat < timePerBeat - playerCoyoteBeat)//Fin CoyoteTime
    //     {
    //         inBeatFlag = false;
    //         endCoyoteTime.Invoke();
    //         if (inMesureFlag)
    //         {
    //             inMesureFlag = false;
    //             endCoyoteAfterMeasure.Invoke();
    //         }
    //     }
    //     
    //     if (!inBeatFlag && timeSinceLastBeat >= timePerBeat - playerCoyoteBeat)//Debut CoyoteTime
    //     {
    //         inBeatFlag = true;
    //         startCoyoteTime.Invoke();
    //         if (beat < musicParameters.beatsPerMeasure) return;
    //         startCoyoteBeforeMeasure.Invoke();
    //     }
    //     
    //     if (timeSinceLastBeat >= timePerBeat)
    //     {
    //         beat++;
    //         Debug.Log( "beat was called");
    //         
    //         onBeat.Invoke();
    //         onEndBeat.Invoke();
    //         if (beat <= musicParameters.beatsPerMeasure) return;
    //         inMesureFlag = true;
    //         onMeasure.Invoke();
    //         beat = 1;
    //         measure++;
    //     }
    // }
}
