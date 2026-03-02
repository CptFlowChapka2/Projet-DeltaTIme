using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class MusicUpdaterWithPlayerLevels : MonoBehaviour
{
    [SerializeField] private InputTrackerAndPatternSpawner player1;
    [SerializeField] private InputTrackerAndPatternSpawner player2;
    private StudioEventEmitter music;
    private float actualP1Level = 0f;
    private float actualP2Level = 0f;

    private void Start()
    {
        music = GetComponent<StudioEventEmitter>();
    }

    public void UpdateMusic()
    {
        if (player1.numberOfSuccesses > actualP1Level)
        {
            actualP1Level++;
        }
        else if (player1.numberOfSuccesses < actualP1Level)
        {
            actualP1Level--;
        }
        
        if (player2.numberOfSuccesses > actualP2Level)
        {
            actualP2Level++;
        }
        else if (player2.numberOfSuccesses < actualP2Level)
        {
            actualP2Level--;
        }
        
        music.SetParameter("Player1Level", actualP1Level);
        music.SetParameter("Player2Level", actualP2Level);
        // float lvl;
        // music.getParameterByName("Player1Level", out lvl);
        // Debug.Log(lvl);
    }
}
