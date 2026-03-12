using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class UpdateMusic : Doer
{
    private MusicManager musicManager;
    private StudioEventEmitter music;

    private int player1NumberOfSuccesses;
    private int player2NumberOfSuccesses;
    private int player1Level;
    private int player2Level;
    private bool onStartCoyoteMeasure;

    private void Start()
    {
        musicManager = (MusicManager)manager;
        music = GetComponent<StudioEventEmitter>();
    }

    private void Update()
    {
        GetAllUsefulParameters();
        UpdateMusicEachMeasure();
        SetAllUsedParameters();
    }

    public override void GetAllUsefulParameters()
    {
        player1NumberOfSuccesses = musicManager.gameManager.GetNumberOfSuccesses(1);
        player2NumberOfSuccesses = musicManager.gameManager.GetNumberOfSuccesses(2);
        onStartCoyoteMeasure = musicManager.onStartCoyoteMeasure;
        player1Level = musicManager.player1Level;
        player2Level = musicManager.player2Level;
    }

    public override void SetAllUsedParameters()
    {
        musicManager.player1Level = player1Level;
        musicManager.player2Level = player2Level;
    }

    public void UpdateMusicEachMeasure()
    {
        if (!onStartCoyoteMeasure) return;
        
        if (player1NumberOfSuccesses > player1Level) player1Level++;
        if (player2NumberOfSuccesses > player2Level) player2Level++;
        if (player1NumberOfSuccesses < player1Level) player1Level--;
        if (player2NumberOfSuccesses < player2Level) player2Level--;
        
        //Debug.Log("player1Level: " + player1Level + ", player2Level: " + player2Level);
        
        music.SetParameter("Player1Level", player1Level);
        music.SetParameter("Player2Level", player2Level);
    }
}
