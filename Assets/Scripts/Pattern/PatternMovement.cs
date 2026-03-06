using System;
using UnityEngine;
using UnityEngine.Events;

public class PatternMovement : MonoBehaviour
{
    private PatternParameters patternParameters;
    private OldBeatClock _oldBeatClock;

    private void Start()
    {
        patternParameters = GetComponent<PatternParameters>();
        _oldBeatClock = GameObject.Find("MusicManager").GetComponent<OldBeatClock>();
        _oldBeatClock.onBeat.AddListener(Advance);
    }

    public void Advance()
    {
        transform.position += transform.forward;
        patternParameters.numberOfBeatsOfLife++;

        switch (patternParameters.numberOfBeatsOfLife)
        {
            case 1:
                SwitchTilesStatesToActive(patternParameters.gridPattern.tilesLine1);
                break;
            case 2:
                SwitchTilesStatesToActive(patternParameters.gridPattern.tilesLine2);
                break;
            case 3:
                SwitchTilesStatesToActive(patternParameters.gridPattern.tilesLine3);
                break;
            case 4:
                SwitchTilesStatesToActive(patternParameters.gridPattern.tilesLine4);
                break;
            case 8:
                SwitchTilesStatesToInvisible(patternParameters.gridPattern.tilesLine1);
                break;
            case 9:
                SwitchTilesStatesToInvisible(patternParameters.gridPattern.tilesLine2);
                break;
            case 10:
                SwitchTilesStatesToInvisible(patternParameters.gridPattern.tilesLine3);
                break;
            case 11:
                SwitchTilesStatesToInvisible(patternParameters.gridPattern.tilesLine4);
                _oldBeatClock.onBeat.RemoveListener(Advance);
                Destroy(gameObject);
                break;
        }
    }

    private void SwitchTilesStatesToActive(LightStateChecker[] tiles)
    {
        foreach (var tile in tiles)
        {
            if (tile.tileState == TileState.Invisible)
            {
                tile.tileState = TileState.Active;
            }
        }
    }
    
    private void SwitchTilesStatesToInvisible(LightStateChecker[] tiles)
    {
        foreach (var tile in tiles)
        {
            if (tile.tileState == TileState.Active)
            {
                tile.tileState = TileState.Invisible;
            }
        }
    }
}
