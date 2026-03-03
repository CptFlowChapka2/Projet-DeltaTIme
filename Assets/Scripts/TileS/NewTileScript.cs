using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewTileScript : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);
    public TileState thisState = TileState.Safe;
    private NewTileScript[,] AllTile = new NewTileScript[,]{};
    public List<ActivatedTileScript> thisPatterneList = new List<ActivatedTileScript>();
    public List<ActivatedTileScript> thisPatternSignList = new List<ActivatedTileScript>();
    public MeshRenderer meshRenderer;
    public Vector2Int patternDirection = new Vector2Int(0, 0);
    public int[] count = new int[] { 0, 0 };
   
    private MeshRenderer[] possiblesArrows;
    private MeshRenderer currentArrow = null;
    
    private void Awake()
    {
        var markers = GetComponentsInChildren<ArrowPlane>();
        possiblesArrows = new MeshRenderer[markers.Length];

        for (int i = 0; i < markers.Length; i++)
        {
            possiblesArrows[i] = markers[i].GetComponent<MeshRenderer>();
        }
    }

    private void Start()
    {
        currentArrow = possiblesArrows[0];
        foreach (MeshRenderer variant in possiblesArrows)
        {
            if (variant != currentArrow)
            {
                variant.enabled = false;
            }
        }
    }

    private void Update()
    {
        count = new[] { thisPatterneList.Count, thisPatternSignList.Count };
    }

    public void Initialize(int x, int y,NewTileScript[,] refToAllTile)
    {
        AllTile = refToAllTile;
        position = new Vector2Int(x, y);
        BeatClock beatClock = FindFirstObjectByType<BeatClock>();
        beatClock.onEndBeat.AddListener(this.CheckForPattern);
    }

    public void CheckForPattern()
    {
        CleanPatternInfoList();

        if (thisState == TileState.Invalid)
        {
            currentArrow = possiblesArrows[0];
            ChangeFeedBackColor(Color.blue);
            return;
        }

        switch (thisPatternSignList.Count)
        {
            case >= 1:
                currentArrow = possiblesArrows[0];
                ChangeFeedBackColor(Color.yellow);
                thisState = TileState.Safe;
                return;
        }

        switch (thisPatterneList.Count)
        {
            case >= 1:
                thisState = TileState.Damaging;
                ChangeFeedBackColor(Color.red);
                currentArrow.enabled = false;
                if (patternDirection == Vector2Int.left)
                {
                    currentArrow = possiblesArrows[1];
                }
                else if (patternDirection == Vector2Int.right)
                {
                    currentArrow = possiblesArrows[2];
                }
                else if (patternDirection == Vector2Int.up)
                {
                    currentArrow = possiblesArrows[3];
                }
                else if (patternDirection == Vector2Int.down)
                {
                    currentArrow = possiblesArrows[4];
                }
                else
                {
                    currentArrow = possiblesArrows[0];
                }
                
                currentArrow.enabled = true;
                
                return;
        }

        ChangeFeedBackColor(Color.antiqueWhite);
        thisState = TileState.Safe;
    }

    private void ChangeFeedBackColor(Color newColor)
    {
        meshRenderer.material.color = newColor;
    }

    public NewTileScript NextTileScript(Vector2Int dir)
    {
        Vector2Int newPositionCoord = position + dir;
        bool newPositionExist = newPositionCoord.x  <= AllTile.GetUpperBound(0) &&
                                newPositionCoord.y <= AllTile.GetUpperBound(1)&&
                                newPositionCoord.x  >= AllTile.GetLowerBound(0) &&
                                newPositionCoord.y >= AllTile.GetLowerBound(1)
            ;
        if (!newPositionExist) return null;
        NewTileScript nextTile = AllTile[newPositionCoord.x, newPositionCoord.y];
        nextTile.patternDirection = patternDirection;
        patternDirection = new Vector2Int(0, 0);
        return nextTile;
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