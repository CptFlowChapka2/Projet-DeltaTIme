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
    private MusicParametersForFMOD _musicParametersForFMOD;
    [SerializeField] private List<List<Vector2Int>> allActivePattern = new List<List<Vector2Int>>();
    [SerializeField] private List<Vector2Int> inputThisMesure = new List<Vector2Int>(); 
    private int maxInputInMesure;
    private ActivatedTIleManager _activatedTIleManager;
    

    private void Start()
    {
        _patternBank = FindAnyObjectByType<PatternBank>();
        _beatClock = FindAnyObjectByType<BeatClock>();
        _gridSpawner = FindAnyObjectByType<NewDanceFloorSpawner>();
        _activatedTIleManager = FindAnyObjectByType<ActivatedTIleManager>();
        _inputPerPlayer = GetComponent<InputPerPlayer>();
        _musicParametersForFMOD = FindAnyObjectByType<MusicParametersForFMOD>();
        maxInputInMesure = _musicParametersForFMOD.beatsPerMeasure;

    }

    private bool alreadyEmptyThisBeat = false;
    public void ReceivePlayerInput(int playerID, bool inCoyote, Vector2Int inputs)
    {
        if (playerID != _inputPerPlayer.playerNumber ) return;
        if(inputThisMesure.Count==maxInputInMesure)return;
        if (!alreadyEmptyThisBeat&&!inCoyote)
        {
            inputThisMesure.Add(Vector2Int.zero);
            alreadyEmptyThisBeat = true;
            return;
        }
        inputThisMesure.Add(inputs);
    }

    public void ReceiveMesure()
    {
        List<Vector2Int> inputThisMesureInstance = new List<Vector2Int>(inputThisMesure);
        allActivePattern.Add(inputThisMesureInstance);
        inputThisMesure.Clear();
    }

    public void ReceiveBeat()
    {
        alreadyEmptyThisBeat = false;
    }
    
    public void SpawnPattern()
    {
        
        allActivePattern.RemoveAll(x => x.Count < 1);
        if(allActivePattern.Count<1)return;
        foreach (List<Vector2Int> inputsThisMesure in allActivePattern)
        {
            NewTileScript[] origne = CreateOrigine(inputsThisMesure);
            if (origne == null) continue;
            CreatePatterneInfo(inputsThisMesure, origne);
            inputsThisMesure.Remove(inputsThisMesure.First());
            
        }
        
    }

    private  NewTileScript[] CreateOrigine(List<Vector2Int> inputsThisMesure)
    {
        
        
        if (inputsThisMesure.Count == 0) return null;
        NewTileScript[] origne = new NewTileScript[] { };
        if (inputsThisMesure.First() == Vector2Int.up)
        {
            
            var list = _playerMovement.invalideTile.FindAll(x => x.position.y == 0);
            list.RemoveAll(x => x.position.x == 0 || x.position.x == _gridSpawner.gridSize - 1);
            origne = list.ToArray();
        }

        if (inputsThisMesure.First() == Vector2Int.down)
        {
            var list = _playerMovement.invalideTile.FindAll(x => x.position.y == _gridSpawner.gridSize - 1);
            list.RemoveAll(x => x.position.x == 0 || x.position.x == _gridSpawner.gridSize - 1);
            origne = list.ToArray();
        }

        if (inputsThisMesure.First() == Vector2Int.left)
        {
            var list = _playerMovement.invalideTile.FindAll(x => x.position.x == _gridSpawner.gridSize - 1);
            list.RemoveAll(x => x.position.y == 0 || x.position.y == _gridSpawner.gridSize - 1);
            origne = list.ToArray();
        }

        if (inputsThisMesure.First() == Vector2Int.right)
        {
            var list = _playerMovement.invalideTile.FindAll(x => x.position.x == 0);
            list.RemoveAll(x => x.position.y == 0 || x.position.y == _gridSpawner.gridSize - 1);
            origne = list.ToArray();
        }
        
        
        
        return origne;
    }

    private void CreatePatterneInfo(List<Vector2Int> inputsThisMesure, NewTileScript[] origne)
    {

        Vector2Int inputDirToProcesses = inputsThisMesure.First();
        if (inputDirToProcesses == Vector2Int.zero)
        {
            //todo=feedback
            return;
        }

        int nbrOfSimilareInputInMesure = inputsThisMesure.FindAll(x => x == inputDirToProcesses).Count;
        bool[] patternToSpawn = _patternBank.GetPatternVariantForAnDir(inputDirToProcesses, nbrOfSimilareInputInMesure);
        for (int i = 0; i < patternToSpawn.Length - 1; i++)
        {
            if (patternToSpawn[i] is false)
            {
                //il n'y as rien à faire spawn donc on passe à la prochaine case
                continue;
            }

            _activatedTIleManager.RequestActivatedTile(origne[i], inputDirToProcesses);
        }
       
    }
}