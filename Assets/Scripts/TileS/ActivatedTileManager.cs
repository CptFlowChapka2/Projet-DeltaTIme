using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivatedTileManager : MonoBehaviour
{
    public int numberOfActivatedTile = 25;
    public GameObject prefab;
    public Vector3 bankPosition = new Vector3(30,5,0);
    private List<ActivatedTileScript> activatedTileBanks = new List<ActivatedTileScript>();
    private List<ActivatedTileScript> availableTileBanks = new List<ActivatedTileScript>();

    [SerializeField] private int nbrOfAvailableTile;

    private OldBeatClock _oldBeatClock;

    private void Start()
    {
        for (int i = 0; i < numberOfActivatedTile; i++)
        {
            GameObject justSpawnedActivatedTile = Instantiate(prefab,bankPosition,Quaternion.identity);
            activatedTileBanks.Add(justSpawnedActivatedTile.GetComponent<ActivatedTileScript>());
        }
        
        availableTileBanks = new List<ActivatedTileScript>(activatedTileBanks);
        activatedTileBanks.ForEach(x=>x.activatedTileManager=this);
        nbrOfAvailableTile = availableTileBanks.Count;
        _oldBeatClock = FindAnyObjectByType<OldBeatClock>();
    }

    public ActivatedTileScript RequestActivatedTile(NewTileScript firstTile , Vector2Int dir)
    {
        ActivatedTileScript givenActivatedTile = availableTileBanks.First();
        if (givenActivatedTile is null)
        {
            Debug.LogError("there is no available ActivatedTIle");
            return null;
        }

        givenActivatedTile.Initialise(firstTile,dir,_oldBeatClock);
        return givenActivatedTile;
    }

    public void RemoveInUsedFromList(ActivatedTileScript toRemove)
    {
        availableTileBanks.Remove(toRemove);
        nbrOfAvailableTile = availableTileBanks.Count;
        
    }
    public void AddNotInUsedFromList(ActivatedTileScript toAdd)
    {
        toAdd.transform.position = bankPosition;
        availableTileBanks.Add(toAdd);
        nbrOfAvailableTile = availableTileBanks.Count;
        
    }
}
