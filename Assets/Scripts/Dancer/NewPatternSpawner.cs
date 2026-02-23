using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewPatternSpawner : MonoBehaviour
{
    private PatternBank _patternBank;
    private NewDanceFloorSpawner _gridSpawner;
    private InputPerPlayer _inputPerPlayer;
    public NewPlayerMovement _playerMovement;
    private BeatClock _beatClock;
    [SerializeField] private List<List<Vector2Int>> allActivePattern = new List<List<Vector2Int>>();
    [SerializeField] private List<Vector2Int> inputThisMesure = new List<Vector2Int>();

    private void Start()
    {
        _patternBank = FindAnyObjectByType<PatternBank>();
        _beatClock = FindAnyObjectByType<BeatClock>();
        _gridSpawner = FindAnyObjectByType<NewDanceFloorSpawner>();
        _inputPerPlayer = GetComponent<InputPerPlayer>();
       
    }

    public void ReceivePlayerInput(int playerID, bool inCoyote, Vector2Int inputs)
    {
        if (playerID != _inputPerPlayer.playerNumber || !inCoyote) return;
        inputThisMesure.Add(inputs);
    }

    public void ReceiveMesure()
    {
        List<Vector2Int> inputThisMesureInstance = inputThisMesure.ToList();
        allActivePattern.Add(inputThisMesureInstance);
        inputThisMesure.Clear();
    }
    
    public void SpawnPattern()
    {
        
        allActivePattern.RemoveAll(x => x.Count < 1);
        if(allActivePattern.Count<1)return;
        Debug.Log(allActivePattern.Count);
        foreach (List<Vector2Int> inputsThisMesure in allActivePattern)
        {
            KeyValuePair<Vector2Int, NewTileScript>[] origne = CreateOrigine(inputsThisMesure);
            if (origne == null) continue;
            Debug.Log("pattern origine assigned");
            CreatePatterneInfo(inputsThisMesure, origne);
            inputsThisMesure.Remove(inputsThisMesure.First());
            
        }
        
    }

    private KeyValuePair<Vector2Int, NewTileScript>[] CreateOrigine(List<Vector2Int> inputsThisMesure)
    {
        
        
        if (inputsThisMesure.Count == 0) return null;
        KeyValuePair<Vector2Int, NewTileScript>[] origne = new KeyValuePair<Vector2Int, NewTileScript>[] { };
        if (inputsThisMesure.First() == Vector2Int.up)
        {
            
            var list = _playerMovement.invalideTile.FindAll(x => x.Value.position.y == 0);
            list.RemoveAll(x => x.Value.position.x == 0 || x.Value.position.x == _gridSpawner.gridSize - 1);
            origne = list.ToArray();
        }

        if (inputsThisMesure.First() == Vector2Int.down)
        {
            var list = _playerMovement.invalideTile.FindAll(x => x.Value.position.y == _gridSpawner.gridSize - 1);
            list.RemoveAll(x => x.Value.position.x == 0 || x.Value.position.x == _gridSpawner.gridSize - 1);
            origne = list.ToArray();
        }

        if (inputsThisMesure.First() == Vector2Int.left)
        {
            var list = _playerMovement.invalideTile.FindAll(x => x.Value.position.x == _gridSpawner.gridSize - 1);
            list.RemoveAll(x => x.Value.position.y == 0 || x.Value.position.y == _gridSpawner.gridSize - 1);
            origne = list.ToArray();
        }

        if (inputsThisMesure.First() == Vector2Int.right)
        {
            var list = _playerMovement.invalideTile.FindAll(x => x.Value.position.x == 0);
            list.RemoveAll(x => x.Value.position.y == 0 || x.Value.position.y == _gridSpawner.gridSize - 1);
            origne = list.ToArray();
        }
        

        
        Debug.Log(origne.Length);
        return origne;
    }

    private void CreatePatterneInfo(List<Vector2Int> inputsThisMesure, KeyValuePair<Vector2Int, NewTileScript>[] origne)
    {
        for (int i = 0; i < _patternBank.AllPatternes[inputsThisMesure.First()].allLines
                 [inputsThisMesure.FindAll(x => x == inputsThisMesure.First()).Count-1 ].Length - 1; i++)
        {
            if (inputsThisMesure.First() == Vector2Int.zero)
            {
                //todo=feedback
                continue;
            }
            if (!_patternBank.AllPatternes[inputsThisMesure.First()]
                    .allLines[inputsThisMesure.FindAll(x => x == inputsThisMesure.First()).Count - 1][i])
                continue;
            Debug.Log(origne.Length +" " +i);
            Debug.Log("patternInfoCreated at " + origne[i].Value.position);

            
           
            origne[i].Value.thisPatterneList.Add(new PatternInfo(_playerMovement.tiles, inputsThisMesure.First(),
                origne[i].Value.position,_beatClock));
        }
    }
}