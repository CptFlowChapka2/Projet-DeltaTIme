using System.Collections.Generic;
using UnityEngine;

public class MajorityVerifier : MonoBehaviour
{
    public enum Direction { Undefined, Left, Right, Up, Down }
    
    private InputPerPlayer inputPerPlayer;
    private List<int> globalInputsDuringLastMeasure = new List<int>();
    
    public Direction majorityDirection = Direction.Undefined;
    public Direction subMajorityDirection = Direction.Undefined;

    private void Start()
    {
        inputPerPlayer = GetComponent<InputPerPlayer>();
    }

    public void ChooseMajority()
    {
        majorityDirection = Direction.Undefined;
        subMajorityDirection = Direction.Undefined;
        
        TakingInputInfosInList();

        int maj = 0;
        int subMaj = 0;

        maj = ChoosingHighestValueAndExcludingItIfIsntAlone();
        subMaj = ChoosingHighestValueAndExcludingItIfIsntAlone();

        ChoosingDirectionsInRegardsOf(maj, subMaj);
        ReinitializingNumberOfInputs();
        
        Debug.Log("Majority : " + majorityDirection + " SubMajority : " + subMajorityDirection);
    }

    private void ReinitializingNumberOfInputs()
    {
        inputPerPlayer.numberOfLeftInputsThisMeasure = 0;
        inputPerPlayer.numberOfRightInputsThisMeasure = 0;
        inputPerPlayer.numberOfUpInputsThisMeasure = 0;
        inputPerPlayer.numberOfDownInputsThisMeasure = 0;
    }
    
    private void ChoosingDirectionsInRegardsOf(int maj, int subMaj)
    {
        switch (maj)
        {
            case 0:
                majorityDirection = Direction.Left;
                break;
            case 1:
                majorityDirection = Direction.Right;
                break;
            case 2:
                majorityDirection = Direction.Up;
                break;
            case 3:
                majorityDirection = Direction.Down;
                break;
            default:
                majorityDirection = Direction.Undefined;
                break;
        }

        switch (subMaj)
        {
            case 0:
                subMajorityDirection = Direction.Left;
                break;
            case 1:
                subMajorityDirection = Direction.Right;
                break;
            case 2:
                subMajorityDirection = Direction.Up;
                break;
            case 3:
                subMajorityDirection = Direction.Down;
                break;
            default:
                subMajorityDirection = Direction.Undefined;
                break;
        }
    }

    private int ChoosingHighestValueAndExcludingItIfIsntAlone()
    {
        int numberMax = 0;
        int highestV = 0;
        int numberOfUnusedInputs = 0;
        
        for (int i = 0; i < globalInputsDuringLastMeasure.Count; i++)
        {
            if (globalInputsDuringLastMeasure[i] > numberMax)
            {
                numberMax = globalInputsDuringLastMeasure[i];
                highestV = i;
            }
            else if (globalInputsDuringLastMeasure[i] == 0)
            {
                numberOfUnusedInputs++;
            }
        }

        if (globalInputsDuringLastMeasure.Count - numberOfUnusedInputs > 1)
        {
            globalInputsDuringLastMeasure[highestV] = 0;
        }
        
        return highestV;
    }

    public void TakingInputInfosInList()
    {
        globalInputsDuringLastMeasure = new List<int>();
        globalInputsDuringLastMeasure.Add(inputPerPlayer.numberOfLeftInputsThisMeasure);
        globalInputsDuringLastMeasure.Add(inputPerPlayer.numberOfRightInputsThisMeasure);
        globalInputsDuringLastMeasure.Add(inputPerPlayer.numberOfUpInputsThisMeasure);
        globalInputsDuringLastMeasure.Add(inputPerPlayer.numberOfDownInputsThisMeasure);
    }
}
