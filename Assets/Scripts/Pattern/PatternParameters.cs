using System;
using UnityEngine;

[Serializable]
public struct GridPattern
{
    public LightStateChecker[] tilesLine1;
    public LightStateChecker[] tilesLine2;
    public LightStateChecker[] tilesLine3;
    public LightStateChecker[] tilesLine4;

    public GridPattern(LightStateChecker[] line1, LightStateChecker[] line2,
        LightStateChecker[] line3, LightStateChecker[] line4)
    {
        tilesLine1 = line1;
        tilesLine2 = line2;
        tilesLine3 = line3;
        tilesLine4 = line4;
    }
}

public class PatternParameters : MonoBehaviour
{
    public GridPattern gridPattern;
}

