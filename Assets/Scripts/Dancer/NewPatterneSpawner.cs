using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewPatterneSpawner : MonoBehaviour
{
    private PatternBank _patternBank;
    private NewDanceFloorSpawner _gridSpawner;
    private InputPerPlayer _inputPerPlayer;
    private NewPlayerMovement _playerMovement;
    private List<List<Vector2Int>> allActivePattern = new List<List<Vector2Int>>();

    private void Start()
    {
        _patternBank = FindAnyObjectByType<PatternBank>();
        _gridSpawner = FindAnyObjectByType<NewDanceFloorSpawner>();
        _inputPerPlayer = GetComponent<InputPerPlayer>();
        _playerMovement = GetComponent<NewPlayerMovement>();
    }

    public void ReceivePlayerInput(int playerID, bool inCoyote, Vector2Int inputs)
    {
        if (playerID != _inputPerPlayer.playerNumber || !inCoyote) return;
        List<Vector2Int> inputThisMesure = new List<Vector2Int>();
        inputThisMesure.Add(inputs);
        allActivePattern.Add(inputThisMesure);
    }

    public void SpawnPattern()
    {
        foreach (List<Vector2Int> inputThisMesure in allActivePattern)
        {
            KeyValuePair<Vector2Int, NewTileScript>[] origne = CreateOrigine(inputThisMesure);
            
            CreatePatterneInfo(inputThisMesure, origne);
            inputThisMesure.Remove(inputThisMesure.First());

        }
    }

    private KeyValuePair<Vector2Int, NewTileScript>[] CreateOrigine(List<Vector2Int> inputThisMesure)
    {
        KeyValuePair<Vector2Int, NewTileScript>[] origne = new KeyValuePair<Vector2Int, NewTileScript>[] { };
        if (inputThisMesure.First() == Vector2Int.up)
        {
            origne = _playerMovement.invalideTile.FindAll(x => x.Key.y == 0).ToArray();
        }

        if (inputThisMesure.First() == Vector2Int.down)
        {
            origne = _playerMovement.invalideTile.FindAll(x => x.Key.y == _gridSpawner.gridSize - 1).ToArray();
        }

        if (inputThisMesure.First() == Vector2Int.left)
        {
            origne = _playerMovement.invalideTile.FindAll(x => x.Key.x == 0).ToArray();
        }

        if (inputThisMesure.First() == Vector2Int.down)
        {
            origne = _playerMovement.invalideTile.FindAll(x => x.Key.x == _gridSpawner.gridSize - 1).ToArray();
        }

        return origne;
    }

    private void CreatePatterneInfo(List<Vector2Int> inputThisMesure,KeyValuePair<Vector2Int, NewTileScript>[] origne)
    {
        for (int i = 0; i < _patternBank.AllPatternes[inputThisMesure.First()].
                 allLines[inputThisMesure.ToList().FindAll(x => x == inputThisMesure.First()).Count-1 ].Length - 1; i++)
        {
            if (!_patternBank.AllPatternes[inputThisMesure.First()].
                    allLines[inputThisMesure.ToList().FindAll(x => x == inputThisMesure.First()).Count-1 ][i]) break;
            origne[i].Value.thisPatterneList.Add(new PatternInfo(_playerMovement.tiles, inputThisMesure.First(),
                origne[i].Value.position));
        }
    }
}