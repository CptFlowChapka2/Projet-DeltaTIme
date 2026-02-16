using System;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceholderSoundManager : MonoBehaviour
{
    private MajorityVerifier majorityVerifier;
    private StudioEventEmitter placeholderSounds;
    private int majorityInputIndex;
    private int relativeNumberOfMeasures = 0;

    private void Start()
    {
        majorityVerifier = GetComponent<MajorityVerifier>();
        placeholderSounds = GetComponent<StudioEventEmitter>();
    }

    public void PlayCorrespondingSound()
    {
        relativeNumberOfMeasures++;
        if (relativeNumberOfMeasures > 4)
        {
            relativeNumberOfMeasures = 1;
        }
        
        switch (majorityVerifier.subMajorityDirection)
        {
            case Direction.Left:
                majorityInputIndex = 0; 
                break;
            case Direction.Right:
                majorityInputIndex = 1;
                break;
            case Direction.Up:
                majorityInputIndex = 2;
                break;
            case Direction.Down:
                majorityInputIndex = 3;
                break;
            default:
                majorityInputIndex = 4;
                break;
        }
        
        placeholderSounds.SetParameter("PlayerSound", relativeNumberOfMeasures + majorityInputIndex * 4);
        placeholderSounds.SetParameter("PlayerSound", 0);
    }
}
