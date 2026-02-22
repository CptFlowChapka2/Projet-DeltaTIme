using System.Collections.Generic;
using UnityEngine;

public class PatternInfo
{
    private static Dictionary<Vector2Int, NewTileScript> allTile;

    private static Dictionary<Vector2Int, NewTileScript> AllTile
    {
        get => allTile;
        set => allTile = value;
    }
    

    private Vector2Int _direction;
    
    private NewTileScript _currentTile;
    private NewTileScript _newTile;
    private Vector2Int _currentTileCoords;
    

    public PatternInfo(Dictionary<Vector2Int, NewTileScript> constructorAllTile,Vector2Int dir,Vector2Int initialCoord,BeatClock beatClock)
    {
        beatClock.onBeat.AddListener(ReceiveBeat);
        AllTile = constructorAllTile;
        _direction = dir;
        CurrentTile = AllTile[initialCoord];
    }

    public NewTileScript CurrentTile
    {
        get => _currentTile;
        set
        {
            _currentTile?.thisPatterneList.Remove(this);
            value.thisPatterneList.Add(this);
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
            _newTile?.thisPatternSignList.Remove(this);
            value?.thisPatternSignList.Add(this);
            _newTile = value;
            _newTile?.CheckForPattern();
            
        }
    }

    private void ReceiveBeat()
    {
        Move();
    }

    public void Move()
    {
        NewTile?.thisPatternSignList.RemoveAll(x=>x==this);
        CurrentTile.thisPatterneList.RemoveAll(x=>x==this);
        if (AllTile.ContainsKey(_currentTileCoords + _direction))
        {
            Debug.Log("pattern at "+_currentTile.position+" is  moving toward "+_direction+" at "+Time.frameCount);
            
            CurrentTile =NewTile;
            return;
        }
        
    }
    
}

