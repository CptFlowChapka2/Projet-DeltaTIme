using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MasterClock : Doer
{
    private MusicManager musicManager;

    private double timeInSeconds;

    private void Start()
    {
        musicManager = (MusicManager)manager;
        musicManager.musicPlaying = GetComponent<StudioEventEmitter>().EventInstance;
    }

    private void FixedUpdate()
    {
        GetAllUsefulParameters();
        GetMusicTimeInSeconds();
        SetAllUsedParameters();
    }

    protected override void GetAllUsefulParameters()
    {
        timeInSeconds = musicManager.timeInSeconds;
    }

    protected override void SetAllUsedParameters()
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
