using System;
using UnityEngine;
using UnityEngine.Events;

public class BeatClock : MonoBehaviour
{
    public int beat = 1;
    public int measure = 1;
    public UnityEvent onBeat;
    public UnityEvent onMeasure;
    private MasterClockByFMOD masterClock;
    private MusicParametersForFMOD musicParameters;
    private int currentBPM;
    private double timePerBeat;

    private void Start()
    {
        masterClock = GetComponent<MasterClockByFMOD>();
        musicParameters = GetComponent<MusicParametersForFMOD>();
        currentBPM = musicParameters.beatsPerMinute;
        timePerBeat = 60.0 / currentBPM;
    }

    private void Update()
    {
        int numberOfPreviousBeats = beat - 1 + (musicParameters.beatsPerMeasure * (measure - 1));
        if (masterClock.timeInSeconds - numberOfPreviousBeats * timePerBeat >= timePerBeat)
        {
            beat++;
            onBeat.Invoke();
            if (beat <= musicParameters.beatsPerMeasure) return;
            beat = 1;
            measure++;
            onMeasure.Invoke();
        }
    }
}
