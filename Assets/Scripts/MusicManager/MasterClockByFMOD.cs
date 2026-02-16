using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MasterClockByFMOD : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicPlaying;
    private int timeInMS;
    public double timeInSeconds;

    private void Start()
    {
        musicPlaying = GetComponent<StudioEventEmitter>().EventInstance;
    }

    private void Update()
    {
        musicPlaying.getTimelinePosition(out timeInMS);
        timeInSeconds = (double)timeInMS / 1000;
    }
}
