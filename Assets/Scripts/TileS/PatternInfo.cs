using System;
using System.Collections.Generic;
using UnityEngine;

public class PatternInfo
{
    private static NewTileScript[,]allTile=new NewTileScript[,]{};
    private bool debugFirstSet = true;


    private Vector2Int _direction;

    private NewTileScript _currentTile;
    private NewTileScript _newTile;
    private Vector2Int _currentTileCoords;
    private BeatClock _beatClock;


    public PatternInfo( NewTileScript[,] constructorAllTile, Vector2Int dir,
        Vector2Int initialCoord, BeatClock beatClock)
    {
        Debug.Log("pattern info was constructed");
        _beatClock = beatClock;
        _beatClock.onBeat.AddListener(ReceiveBeat);
        //Ici on copie l'array multidimensionnele //
        AllTile = new NewTileScript[constructorAllTile.GetLength(0), constructorAllTile.GetLength(1)]; //on s'assure que AllTile fait la bonne taille pour recevoir les élement
        Array.Copy(constructorAllTile,AllTile,constructorAllTile.Length);// puis on copie 
        //
        debugFirstSet = false;
        _direction = dir;
        CurrentTile = AllTile[initialCoord.x,initialCoord.y];
    }

    private NewTileScript[,] AllTile
    {
        get => allTile;
        set { allTile = value; }
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
            Vector2Int newPositionCoord = _currentTileCoords + _direction;
            bool newPositionExist = newPositionCoord.x  <= AllTile.GetUpperBound(0) &&
                                    newPositionCoord.y <= AllTile.GetUpperBound(1)&&
                                    newPositionCoord.x  >= AllTile.GetLowerBound(0) &&
                                    newPositionCoord.y >= AllTile.GetLowerBound(1)
                                    ;
            if (!newPositionExist) return;
            NewTile = AllTile[newPositionCoord.x,newPositionCoord.y];
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
        NewTile?.thisPatternSignList.Remove( this);
        CurrentTile.thisPatterneList.Remove( this);
        Vector2Int newPositionCoord = _currentTileCoords + _direction;
        bool newPositionExist = newPositionCoord.x  <= AllTile.GetUpperBound(0) &&
                                newPositionCoord.y <= AllTile.GetUpperBound(1)&&
                                newPositionCoord.x  >= AllTile.GetLowerBound(0) &&
                                newPositionCoord.y >= AllTile.GetLowerBound(1)
            ;
        if (newPositionExist) 
        {
            CurrentTile = NewTile;
            return;
        }

        _beatClock.onBeat.RemoveListener(ReceiveBeat);
    }
}