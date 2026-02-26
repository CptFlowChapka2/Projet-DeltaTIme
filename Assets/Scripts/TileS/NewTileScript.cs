using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewTileScript : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);
    public TileState thisState = TileState.Safe;
    public List<PatternInfo> thisPatterneList = new List<PatternInfo>();
    public List<PatternInfo> thisPatternSignList = new List<PatternInfo>();
    public MeshRenderer colorFeedback;
    public int[] count = new int[] { 0, 0 };

    private void Update()
    {
        count = new[] { thisPatterneList.Count, thisPatternSignList.Count };
    }

    public void Initialize(int x, int y)
    {
        position = new Vector2Int(x, y);
        BeatClock beatClock = FindFirstObjectByType<BeatClock>();
        beatClock.onEndBeat.AddListener(this.CheckForPattern);
    }

    public void CheckForPattern()
    {
        CleanPatternInfoList();

        if (thisState == TileState.Invalid) return;

        switch (thisPatternSignList.Count)
        {
            case >= 1:
                ChangeFeedBackColor(Color.yellow);
                thisState = TileState.Safe;
                return;
        }

        switch (thisPatterneList.Count)
        {
            case >= 1:
                ChangeFeedBackColor(Color.red);
                thisState = TileState.Damaging;
                return;
        }

        ChangeFeedBackColor(Color.antiqueWhite);
        thisState = TileState.Safe;
    }

    private void ChangeFeedBackColor(Color newColor)
    {
        colorFeedback.material.color = newColor;
    }

    //helpers:

    private void CleanPatternInfoList()
    {
        thisPatterneList.RemoveAll(x => x == null);
        thisPatternSignList.RemoveAll(x => x == null);
        thisPatterneList.TrimExcess();
        thisPatternSignList.TrimExcess();
    }

    public enum TileState
    {
        Safe,
        Damaging,
        Invalid
    }
}