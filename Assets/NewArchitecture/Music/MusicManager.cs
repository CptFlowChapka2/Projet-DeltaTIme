using System;
using UnityEngine;

public class MusicManager : Manager
{
    
    private MasterClock masterClock;
    private BeatClock beatClock;

    private void Awake()
    {
        GetAllDoersOnGameObject();
        InitializeDoer<MasterClock>(out masterClock);
        InitializeDoer<BeatClock>(out beatClock);
    }

    [Header("Music Parameters")]
    [SerializeField] public int beatsPerMinute;
    [SerializeField] public int beatsPerMeasure;
    
    [Header("Master Clock")]
    public FMOD.Studio.EventInstance musicPlaying;
    public double timeInSeconds;
    
    [Header("Beat Clock")]
    public int beat = 1;
    public int measure = 1;
    public double coyoteTime = 0.1;
    
    public bool onStartCoyoteBeat = false;
    public bool inCoyoteBeat = false;
    public bool onBeat = false;
    public bool onEndCoyoteBeat = false;
    public bool onStartCoyoteMeasure = false;
    public bool inCoyoteMeasure = false;
    public bool onMeasure = false;
    public bool onEndCoyoteMeasure = false;
}
