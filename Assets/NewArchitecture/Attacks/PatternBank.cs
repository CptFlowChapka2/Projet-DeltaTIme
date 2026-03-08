using System;
using System.Collections.Generic;
using UnityEngine;

public class PatternBank : MonoBehaviour
{
    public Pattern patternLeft=new Pattern();
    public Pattern patternRight=new Pattern();
    public Pattern patternUp=new Pattern();
    public Pattern patternDown=new Pattern();

    public Dictionary<Vector2Int, Pattern> AllPatternes = new Dictionary<Vector2Int, Pattern>();

    private void Awake()
    {
        
        patternLeft.allLines = new List<bool[]>();
        patternLeft.allLines.Add(patternLeft.line1);
        patternLeft.allLines.Add(patternLeft.line2);
        patternLeft.allLines.Add(patternLeft.line3);
        patternLeft.allLines.Add(patternLeft.line4);
        
        patternDown.allLines = new List<bool[]>();
        patternDown.allLines.Add(patternDown.line1);
        patternDown.allLines.Add(patternDown.line2);
        patternDown.allLines.Add(patternDown.line3);
        patternDown.allLines.Add(patternDown.line4);
        
        patternUp.allLines = new List<bool[]>();
        patternUp.allLines.Add(patternUp.line1);
        patternUp.allLines.Add(patternUp.line2);
        patternUp.allLines.Add(patternUp.line3);
        patternUp.allLines.Add(patternUp.line4);
        
        patternRight.allLines = new List<bool[]>();
        patternRight.allLines.Add(patternRight.line1);
        patternRight.allLines.Add(patternRight.line2);
        patternRight.allLines.Add(patternRight.line3);
        patternRight.allLines.Add(patternRight.line4);
        
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
        Debug.Log(allLines.Count);
    }
}