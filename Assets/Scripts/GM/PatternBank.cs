using System;
using System.Collections.Generic;
using UnityEngine;

public class PatternBank : MonoBehaviour
{
    public Pattern patternLeft;
    public Pattern patternRight;
    public Pattern patternUp;
    public Pattern patternDown;

    public Dictionary<Vector2Int, Pattern> AllPatternes = new Dictionary<Vector2Int, Pattern>();

    private void Awake()
    {
        AllPatternes.Add(Vector2Int.up, patternUp);
        AllPatternes.Add(Vector2Int.left, patternLeft);
        AllPatternes.Add(Vector2Int.right, patternRight);
        AllPatternes.Add(Vector2Int.down, patternDown);
    }
}

[Serializable]
public struct Pattern
{
    public bool[] line1;
    public bool[] line2; 
    public bool[] line3; 
    public bool[] line4;

    public List<bool[]> allLines;

    public Pattern(bool[] line1Array, bool[] line2Array, bool[] line3Array, bool[] line4Array)
    {
        allLines = new List<bool[]>();
        line1 = line1Array;
        line2 = line2Array;
        line3 = line3Array;
        line4 = line4Array;
        allLines.Add(line1);
        allLines.Add(line2);
        allLines.Add(line3);
        allLines.Add(line4);
    }
}