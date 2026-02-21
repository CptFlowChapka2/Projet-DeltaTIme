using System;
using UnityEngine;
using UnityEngine.Events;

public class BeatClock : MonoBehaviour
{
    public int beat = 1;
    public int measure = 1;
    public UnityEvent onBeat=new UnityEvent();
    public UnityEvent onEndBeat=new UnityEvent();
    public UnityEvent onMeasure=new UnityEvent();
    public UnityEvent startCoyoteTime=new UnityEvent();
    public UnityEvent endCoyoteTime=new UnityEvent();
    private MasterClockByFMOD masterClock;
    private MusicParametersForFMOD musicParameters;
    private int currentBPM;
    private double timePerBeat;
    [SerializeField] private float playerCoyoteBeat=0.25f;

    private void Start()
    {
        masterClock = GetComponent<MasterClockByFMOD>();
        musicParameters = GetComponent<MusicParametersForFMOD>();
        currentBPM = musicParameters.beatsPerMinute;
        timePerBeat = 60.0 / currentBPM;
        EventSyncroniser eventSyncroniser = FindAnyObjectByType<EventSyncroniser>();
        startCoyoteTime.AddListener(eventSyncroniser.ReceiveStartCoyote);
        endCoyoteTime.AddListener(eventSyncroniser.ReceiveEndCoyote);
    }

    private void Update()
    {
        int numberOfPreviousBeats = beat - 1 + (musicParameters.beatsPerMeasure * (measure - 1));

        if (masterClock.timeInSeconds - numberOfPreviousBeats * timePerBeat >= timePerBeat - playerCoyoteBeat)//Debut CoyoteTime
        {
            startCoyoteTime.Invoke();
        }
        if (masterClock.timeInSeconds - numberOfPreviousBeats * timePerBeat >= timePerBeat + playerCoyoteBeat)//Fin CoyoteTime
        {
            endCoyoteTime.Invoke();
            
        }
        if (masterClock.timeInSeconds - numberOfPreviousBeats * timePerBeat >= timePerBeat)
        {
            beat++;
            Debug.Log( "beat was called");
            
            onBeat.Invoke();
            onEndBeat.Invoke();
            if (beat <= musicParameters.beatsPerMeasure) return;
            beat = 1;
            measure++;
            onMeasure.Invoke();
        }
    }
}
