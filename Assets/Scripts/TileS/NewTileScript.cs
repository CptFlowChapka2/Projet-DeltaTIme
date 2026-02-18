using System;
using System.Collections.Generic;
using UnityEngine;

public class NewTileScript : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);
    public TileState thisState=TileState.Safe;
    public List<PatternInfo> thisPatterneList;
    public List<PatternInfo> thisPatternSignList;
    
    public void Initialize(int x, int y)
    {
        position = new Vector2Int(x, y);
    }


    private void MovePaterne()
    {
        thisPatterneList.ForEach(x=>x.Move());
    }

    public void CheckForPattern()
    {
    }
    
    public enum TileState
    {
       Safe,
       Damaging,
       Invalid
    }
}

