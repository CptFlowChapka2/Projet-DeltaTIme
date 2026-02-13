using System;
using UnityEngine;

public class PatternBank : MonoBehaviour
{
    public Pattern patternLeft;
    public Pattern patternRight;
    public Pattern patternUp;
    public Pattern patternDown;
}

[Serializable]
public struct Pattern
{
    public bool[] line1;
    public bool[] line2; 
    public bool[] line3; 
    public bool[] line4; 

    public Pattern(bool[] line1Array, bool[] line2Array, bool[] line3Array, bool[] line4Array)
    {
        line1 = line1Array;
        line2 = line2Array;
        line3 = line3Array;
        line4 = line4Array;
    }
}