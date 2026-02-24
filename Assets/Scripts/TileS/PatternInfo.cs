using System.Collections.Generic;
using UnityEngine;

public class PatternInfo
{
   
    private static Dictionary<Vector2Int, NewTileScript> allTile;
    private bool debugFirstSet = true;
   
    

    private Vector2Int _direction;
    
    private NewTileScript _currentTile;
    private NewTileScript _newTile;
    private Vector2Int _currentTileCoords;
    private BeatClock _beatClock;
    

    
    public PatternInfo(Dictionary<Vector2Int, NewTileScript> constructorAllTile,Vector2Int dir,Vector2Int initialCoord,BeatClock beatClock)
    {
        
        _beatClock = beatClock;
        _beatClock.onBeat.AddListener(ReceiveBeat);
        AllTile = new Dictionary<Vector2Int, NewTileScript>(constructorAllTile);
        debugFirstSet = false;
        _direction = dir;
        CurrentTile = AllTile[initialCoord];
    }
 private Dictionary<Vector2Int, NewTileScript> AllTile
    {
        get => allTile;
        set
        {
            allTile = value;
        }
    }
    
    public NewTileScript CurrentTile
    {
        get => _currentTile;
        set
        {
             value.thisPatterneList.Add(this);
            _currentTile?.thisPatterneList.Remove(this);    
            _currentTile = value;
            _currentTileCoords = _currentTile.position;
            _currentTile.CheckForPattern();
            if(!AllTile.ContainsKey(_currentTileCoords+_direction)) return;
            NewTile = AllTile[_currentTileCoords+_direction];
        }
    }
    private NewTileScript NewTile
    {
        get => _newTile;
        set
        {
            value?.thisPatternSignList.Add(this);
            _newTile?.thisPatternSignList.Remove(this);
            
            _newTile = value;
            _newTile?.CheckForPattern();
            
        }
    }

    private void ReceiveBeat()
    {
        // foreach (var newTileScript in AllTile)
        // {
        //     Debug.Log(newTileScript.Key +" "+newTileScript.Value.GetEntityId());
        // }

        Move();
    }

    public void Move()
    {
        NewTile?.thisPatternSignList.RemoveAll(x=>x==this);
        CurrentTile.thisPatterneList.RemoveAll(x=>x==this);
        if (AllTile.ContainsKey(_currentTileCoords + _direction))
        {
            CurrentTile =NewTile;
            return;
        }
       _beatClock.onBeat.RemoveListener(ReceiveBeat);
    }
    
}

