using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class MusicUpdater : MonoBehaviour
{
    [SerializeField] private FeedbacksManager feedbacksManager;
    private StudioEventEmitter music;

    private void Start()
    {
        music = GetComponent<StudioEventEmitter>();
    }

    public void UpdateMusicEachMeasure()
    {
        music.SetParameter("Player1Level", feedbacksManager.actualP1Level);
        music.SetParameter("Player2Level", feedbacksManager.actualP2Level);
    }
}
