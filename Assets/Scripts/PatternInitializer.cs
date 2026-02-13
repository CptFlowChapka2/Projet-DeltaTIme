using System;
using UnityEngine;

public class PatternInitializer : MonoBehaviour
{
    public int playerNumber;
    private MajorityVerifier majorityVerifier;
    private PatternParameters patternParameters;
    private Direction dirMaj;
    
    private void Start()
    {
        if (playerNumber == 1)
        {
            majorityVerifier = GameObject.Find("Dancer1").GetComponent<MajorityVerifier>();
        }
        else if (playerNumber == 2)
        {
            majorityVerifier = GameObject.Find("Dancer2").GetComponent<MajorityVerifier>();
        }
        
        patternParameters = GetComponent<PatternParameters>();
        

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
