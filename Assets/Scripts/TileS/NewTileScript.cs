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
    public int[] count = new int[] { 0, 0 };

    public Material[] posssibleMaterial;
   
    private enum matNameToIndex
    {
        Nothing,
        Up,
        Down,
        Left,
        Right,
        RightLeft,
        UpDown,
        DownLeft,
        DownRight,
        UpLeft,
        Upright,
        All,
        TempSign,
        Invalide,
    }

    private void Start()
    {
        ChangeFeedBackColor(matNameToIndex.Nothing);
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
            ChangeFeedBackColor(matNameToIndex.Invalide);
            return;
        }

        
        if (thisPatterneList.Count>=1)
        {
            
            matNameToIndex selectorEnum=PatternDirectionCounter(thisPatterneList);
            ChangeFeedBackColor(selectorEnum);
            thisState = TileState.Damaging;
            return;
           
        }
        switch (thisPatternSignList.Count)
        {
            case >= 1:
                ChangeFeedBackColor(matNameToIndex.TempSign);
                thisState = TileState.Safe;
                return;
        }


        ChangeFeedBackColor(matNameToIndex.Nothing);
        thisState = TileState.Safe;
    }

    private void ChangeFeedBackColor(matNameToIndex tochange)
    {
        meshRenderer.material = posssibleMaterial[(int)tochange];
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
        return AllTile[newPositionCoord.x, newPositionCoord.y];
    }

    //helpers:

    private void CleanPatternInfoList()
    {
        thisPatterneList.RemoveAll(x => x == null);
        thisPatternSignList.RemoveAll(x => x == null);
        thisPatterneList.TrimExcess();
        thisPatternSignList.TrimExcess();
    }

    private matNameToIndex PatternDirectionCounter(List<ActivatedTileScript> ATSs)
    {
        bool[] counter = new bool[4] { false, false, false, false };

        foreach (var atS in ATSs)
        {
            if (atS.direction==Vector2Int.up)
            {
                counter[0] = true;
            }
            else if (atS.direction==Vector2Int.down)
            {
                counter[1] = true;
            }
            else if (atS.direction==Vector2Int.left)
            {
                counter[2] = true;
            }
            else if (atS.direction==Vector2Int.right)
            {
                counter[3] = true;
            }
        }//on verifie l'existence pour chaque direction
        
        if (counter[0] && counter[1] && counter[2] && counter[3]) return matNameToIndex.All;
        if (counter[0] && counter[1] && !counter[2] && !counter[3]) return matNameToIndex.UpDown;
        if (!counter[0] && !counter[1] && counter[2] && counter[3]) return matNameToIndex.RightLeft;
        if (counter[0] && !counter[1] && !counter[2] && counter[3]) return matNameToIndex.Upright;
        if (counter[0] && !counter[1] && counter[2] && !counter[3]) return matNameToIndex.UpLeft;
        if (!counter[0] && counter[1] && !counter[2] && counter[3]) return matNameToIndex.DownRight;
        if (!counter[0] && counter[1] && counter[2] && !counter[3]) return matNameToIndex.DownLeft;
        if (counter[0] && !counter[1] && !counter[2] && !counter[3]) return matNameToIndex.Up;
        if (!counter[0] && counter[1] && !counter[2] && !counter[3]) return matNameToIndex.Down;
        if (!counter[0] && !counter[1] && counter[2] && !counter[3]) return matNameToIndex.Left;
        if (!counter[0] && !counter[1] && !counter[2] && counter[3]) return matNameToIndex.Right;

        //c'est dégeulasse mais apparamnet unity est en C# avant version 11ou 14 donc on peux pas faire de pattern match avec des array donc pas de switch donc on souffre
        return matNameToIndex.Nothing;

    }

    

    public enum TileState
    {
        Safe,
        Damaging,
        Invalid
    }
}