using System;
using UnityEngine;

public class BeatClock : Doer
{
    private MusicManager musicManager;
    
    private double timeInSeconds;
    private int beat;
    private int measure;
    private double coyoteTime;
    private int beatsPerMeasure;
    private int beatsPerMinute;
    
    private bool onStartCoyoteBeat;
    private bool inCoyoteBeat;
    private bool onBeat;
    private bool onEndCoyoteBeat;
    private bool onStartCoyoteMeasure;
    private bool inCoyoteMeasure;
    private bool onMeasure;
    private bool onEndCoyoteMeasure;

    private void Awake()
    {
        musicManager = (MusicManager)manager;
    }

    private void FixedUpdate()
    {
        GetAllUsefulParameters();
        //ReinitializeFlags();
        SequenceBeatsAndMeasures();
        SetAllUsedParameters();
    }

    public override void GetAllUsefulParameters()
    {
        timeInSeconds = musicManager.timeInSeconds;
        beat = musicManager.beat;
        measure = musicManager.measure;
        coyoteTime = musicManager.coyoteTime;
        beatsPerMeasure = musicManager.beatsPerMeasure;
        beatsPerMinute = musicManager.beatsPerMinute;
        //onStartCoyoteBeat = musicManager.onStartCoyoteBeat;
        inCoyoteBeat = musicManager.inCoyoteBeat;
        //onBeat = musicManager.onBeat;
        //onEndCoyoteBeat = musicManager.onEndCoyoteBeat;
        //onStartCoyoteMeasure = musicManager.onStartCoyoteMeasure;
        inCoyoteMeasure = musicManager.inCoyoteMeasure;
        //onMeasure = musicManager.onMeasure;
        //onEndCoyoteMeasure = musicManager.onEndCoyoteMeasure;
    }

    public override void SetAllUsedParameters()
    {
        musicManager.timeInSeconds = timeInSeconds;
        musicManager.beat = beat;
        musicManager.measure = measure;
        musicManager.coyoteTime = coyoteTime;
        musicManager.beatsPerMeasure = beatsPerMeasure;
        musicManager.beatsPerMinute = beatsPerMinute;
        //musicManager.onStartCoyoteBeat = onStartCoyoteBeat;
        musicManager.inCoyoteBeat = inCoyoteBeat;
        //musicManager.onBeat = onBeat;
        //musicManager.onEndCoyoteBeat = onEndCoyoteBeat;
        //musicManager.onStartCoyoteMeasure = onStartCoyoteMeasure;
        musicManager.inCoyoteMeasure = inCoyoteMeasure;
        //musicManager.onMeasure = onMeasure;
        //musicManager.onEndCoyoteMeasure = onEndCoyoteMeasure;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void SequenceBeatsAndMeasures()
    {
        int numberOfPreviousBeats = beat - 1 + (beatsPerMeasure * (measure - 1));
        double timePerBeat = 60.0 / beatsPerMinute;
        double timeSinceLastBeat = timeInSeconds - numberOfPreviousBeats * timePerBeat;
        
        if (timeInSeconds < timePerBeat)
        {
            beat = 1;
            measure = 1;
        }
        
        if (inCoyoteBeat && timeSinceLastBeat >= coyoteTime && timeSinceLastBeat < timePerBeat - coyoteTime)//Fin CoyoteTime
        {
            inCoyoteBeat = false;
            musicManager.gameManager.onEndCoyoteBeat.Invoke();
            //onEndCoyoteBeat = true;
            if (inCoyoteMeasure)
            {
                inCoyoteMeasure = false;
                musicManager.gameManager.onEndCoyoteMeasure.Invoke();
                //onEndCoyoteMeasure = true;
            }

            if (beat < beatsPerMeasure) return;
            musicManager.gameManager.onBeginningMeasure.Invoke();
        }
        
        if (!inCoyoteBeat && timeSinceLastBeat >= timePerBeat - coyoteTime)//Debut CoyoteTime
        {
            inCoyoteBeat = true;
            musicManager.gameManager.onStartCoyoteBeat.Invoke();
            //onStartCoyoteBeat = true;
            if (beat < beatsPerMeasure) return;
            inCoyoteMeasure = true;
            musicManager.gameManager.onStartCoyoteMeasure.Invoke();
            //onStartCoyoteMeasure = true;
        }
        
        if (timeSinceLastBeat >= timePerBeat)
        {
            beat++;
            musicManager.gameManager.onBeat.Invoke();
            //onBeat = true;
            if (beat <= beatsPerMeasure) return;
            musicManager.gameManager.onMeasure.Invoke();
            //onMeasure = true;
            beat = 1;
            measure++;
        }
    }

    // private void ReinitializeFlags()
    // {
    //     onBeat = false;
    //     onMeasure = false;
    //     onStartCoyoteBeat = false;
    //     onStartCoyoteMeasure = false;
    //     onEndCoyoteBeat = false;
    //     onEndCoyoteMeasure = false;
    // }
}
