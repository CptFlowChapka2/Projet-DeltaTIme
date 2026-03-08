using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MasterClock : Doer
{
    private MusicManager musicManager;

    private double timeInSeconds;

    private void Awake()
    {
        musicManager = (MusicManager)manager;
    }

    private void Start()
    {
        musicManager.musicPlaying = GetComponent<StudioEventEmitter>().EventInstance;
    }

    private void FixedUpdate()
    {
        GetAllUsefulParameters();
        GetMusicTimeInSeconds();
        SetAllUsedParameters();
    }

    public override void GetAllUsefulParameters()
    {
        timeInSeconds = musicManager.timeInSeconds;
    }

    public override void SetAllUsedParameters()
    {
        musicManager.timeInSeconds = timeInSeconds;
    }

    private void GetMusicTimeInSeconds()
    {
        int timeInMS;
        musicManager.musicPlaying.getTimelinePosition(out timeInMS);
        timeInSeconds = (double)timeInMS / 1000;
    }
}
