using System;
using UnityEngine;

public class PatternInitializer : MonoBehaviour
{
    public int playerNumber;
    private MajorityVerifier majorityVerifier;
    private PatternParameters patternParameters;
    private PatternBank patternBank;
    private Direction dirMaj;
    private Direction subDirMaj;
    
    private void Start()
    {
        SearchingForAppropriateParameters();
        DetermineDirectionOfIncidence();
        
        Pattern patternChoice = AppropriatePatternInstance();
        GridPattern gridPattern = patternParameters.gridPattern;

        for (int i = 0; i < 7; i++)
        {
            ChangingLightState(patternChoice.line1, gridPattern.tilesLine1, i);
            ChangingLightState(patternChoice.line2, gridPattern.tilesLine2, i);
            ChangingLightState(patternChoice.line3, gridPattern.tilesLine3, i);
            ChangingLightState(patternChoice.line4, gridPattern.tilesLine4, i);
        }
    }

    private static void ChangingLightState(bool[] boolLine, LightStateChecker[] lightLine, int i)
    {
        if (boolLine[i] == true)
        {
            lightLine[i].tileState = TileState.Invisible;
        }
        else
        {
            lightLine[i].tileState = TileState.Unused;
        }
    }

    private Pattern AppropriatePatternInstance()
    {
        Pattern patternChoice = default;
        subDirMaj = majorityVerifier.subMajorityDirection;

        switch (subDirMaj)
        {
            case Direction.Left:
                patternChoice = patternBank.patternLeft;
                break;
            case Direction.Right:
                patternChoice = patternBank.patternRight;
                break;
            case Direction.Up:
                patternChoice = patternBank.patternUp;
                break;
            case Direction.Down:
                patternChoice = patternBank.patternDown;
                break;
            default:
                break;
        }

        return patternChoice;
    }

    private void SearchingForAppropriateParameters()
    {
        if (playerNumber == 1)
        {
            majorityVerifier = GameObject.Find("Dancer1").GetComponent<MajorityVerifier>();
        }
        else if (playerNumber == 2)
        {
            majorityVerifier = GameObject.Find("Dancer2").GetComponent<MajorityVerifier>();
        }
        
        patternBank = GameObject.Find("GM").GetComponent<PatternBank>();
        patternParameters = GetComponent<PatternParameters>();
    }

    private void DetermineDirectionOfIncidence()
    {
        dirMaj = majorityVerifier.majorityDirection;

        switch (dirMaj)
        {
            case Direction.Left :
                transform.Rotate(new Vector3(0, 270, 0));
                break;
            case Direction.Right :
                transform.Rotate(new Vector3(0, 90, 0));
                break;
            case Direction.Up :
                break;
            case Direction.Down :
                transform.Rotate(new Vector3(0, 180, 0));
                break;
            default:
                break;
        }
    }
}
