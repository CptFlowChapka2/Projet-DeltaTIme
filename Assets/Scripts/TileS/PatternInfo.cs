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

    public PatternInfo(Dictionary<Vector2Int, NewTileScript> constructorAllTile,Vector2Int dir,Vector2Int initialCoord)
    {
        AllTile = constructorAllTile;
        _direction = dir;
        CurrentTile = AllTile[initialCoord];
    }

    private NewTileScript CurrentTile
    {
        get => _currentTile;
        set
        {
            _currentTile?.thisPatterneList.Remove(this);
            value.thisPatterneList.Add(this);
            _currentTile = value;
            _currentTileCoords = _currentTile.position;
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
            
        }
    }

    public void Move()
    {
        if (AllTile[_currentTileCoords + _direction] !=null)
        {
            CurrentTile =NewTile;
            return;
        }

        NewTile?.thisPatternSignList.Remove(this);
        CurrentTile.thisPatterneList.Remove(this);

    }
    
}

