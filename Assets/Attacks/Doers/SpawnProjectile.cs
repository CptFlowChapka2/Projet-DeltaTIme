using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class SpawnProjectile : Doer
{
    public AttackMode mode;
    private AttacksManager attacksManager;
    private bool onMesureFlag;
    private bool onBeatFlag;
    private List<List<Vector2Int>> allInputsThisMeasureP1 = new List<List<Vector2Int>>();
    private List<List<Vector2Int>> allInputsThisMeasureP2 = new List<List<Vector2Int>>();
    private List<TileId> allInvalideTileP1 = new List<TileId>();
    private List<TileId> allInvalideTileP2 = new List<TileId>();
    
    private int danceFloorSize;
    private void Awake()
    {
        attacksManager = (AttacksManager)manager;
    }

    private void Start()
    {
        danceFloorSize = attacksManager.gameManager.GetDanceFloorSize()+2;
        allInvalideTileP1 = attacksManager.gameManager.GetDanceFloorAllInvalidTile(1);
        allInvalideTileP2 = attacksManager.gameManager.GetDanceFloorAllInvalidTile(2);
    }

    private void FixedUpdate()
    {
        allInputsThisMeasureP1.RemoveAll(x =>x.Count.Equals(0));
        allInputsThisMeasureP2.RemoveAll(x =>x.Count.Equals(0));
        allInputsThisMeasureP1.TrimExcess();
        allInputsThisMeasureP2.TrimExcess();
    }

    private void SpawnAProjectile(int playerID)
    {
        List<List<Vector2Int>> playerInputToProcees = playerID switch
        {
            1=>allInputsThisMeasureP1,
            2=>allInputsThisMeasureP2,
            _ => throw new ArgumentOutOfRangeException(nameof(playerID), playerID, null)
        };
        if (playerInputToProcees.Count==0)return ; 
        
        List<TileId> allInvalidTile = playerID switch
        {
            1=>allInvalideTileP1,
            2=>allInvalideTileP2,
            _ => throw new ArgumentOutOfRangeException(nameof(playerID), playerID, null)
        };
        
        foreach (var inputsThisMeasure in playerInputToProcees)
        {
            if(inputsThisMeasure.Count==0)continue;
            Vector2Int inputsThisMeasureFirst = inputsThisMeasure.First();
            if (inputsThisMeasureFirst == Vector2Int.zero || inputsThisMeasureFirst == new Vector2Int(-1, -1))
            {
                inputsThisMeasure.Remove(inputsThisMeasureFirst);
                continue;
            };
            //Debug.Log("projectile was called to be spawned");
            AssignProjectileId(inputsThisMeasure,allInvalidTile,playerID);
            inputsThisMeasure.Remove(inputsThisMeasure.First());
        }
    }
    
    private void AssignProjectileId(List<Vector2Int> inputsThisMesure, List<TileId> invalideTiles, int playerId)
    {

        Vector2Int inputDirToProcesses = inputsThisMesure.First();
        if (inputDirToProcesses == Vector2Int.zero)
        {
            //Debug.Log("projectile was Zero dir and thus canceled");
            //todo=feedback
            return;
        }

        invalideTiles = CurateInvalide(invalideTiles,inputDirToProcesses);

        int invertedPlayerId = playerId switch
        {
            1=>2,
            2=>1,
            _ => throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null)
        };

        Vector2Int playerCoords = attacksManager.gameManager.GetPlayerCurrentRelativeCoords(playerId);
        Vector2Int invertedPlayerCoords = attacksManager.gameManager.GetPlayerCurrentRelativeCoords(invertedPlayerId);
        TileId target = mode switch
        {
            AttackMode.AtoA => invalideTiles.Find(
                x=>x.position.x.Equals(playerCoords.x)|| x.position.y.Equals(playerCoords.y)),
            AttackMode.BtoA =>   invalideTiles.Find(
                x=>x.position.x.Equals(invertedPlayerCoords.x)|| x.position.y.Equals(invertedPlayerCoords.y)),
            _ => throw new ArgumentOutOfRangeException()
        };
        RequestProjectileID(target, inputDirToProcesses, playerId);
    }
    
    public ProjectileID RequestProjectileID(TileId firstTile , Vector2Int dir,int playerID)
    {
        ProjectileID projectileID = attacksManager.allProjectileIds.Find(x=>!x.inUse);
        if (projectileID is null)
        {
            return null;
        }

        int opponentId = playerID switch
        {
            1 => 2,
            2 => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(playerID), playerID, null)
        };
        
        projectileID.PutInUse(dir,firstTile,opponentId);
        return projectileID;
    }

    public void ListenForOnBeginningMeasure()
    {
        allInputsThisMeasureP1.Add(attacksManager.gameManager.GetInputsThisMeasure(1));
        allInputsThisMeasureP2.Add(attacksManager.gameManager.GetInputsThisMeasure(2));
    }

    private List<TileId> CurateInvalide(List<TileId> uncuratedInvalideList,Vector2Int inputsThisMesure)
    {
        List<TileId> list = new List<TileId>();
        if (inputsThisMesure == Vector2Int.up)
        {
             list = uncuratedInvalideList.FindAll(x => x.position.y == 0);
            list.RemoveAll(x => x.position.x == 0 || x.position.x == danceFloorSize - 1);
            

        }
        else if (inputsThisMesure == Vector2Int.down)
        {
             list = uncuratedInvalideList.FindAll(x => x.position.y == danceFloorSize - 1);
            list.RemoveAll(x => x.position.x == 0 || x.position.x == danceFloorSize- 1);
            
        }

        else if (inputsThisMesure == Vector2Int.left)
        {
             list = uncuratedInvalideList.FindAll(x => x.position.x == danceFloorSize - 1);
            list.RemoveAll(x => x.position.y == 0 || x.position.y == danceFloorSize - 1);
                    
        }

        else if (inputsThisMesure == Vector2Int.right)
        {
            
             list = uncuratedInvalideList.FindAll(x => x.position.x == 0);
            list.RemoveAll(x => x.position.y == 0 || x.position.y == danceFloorSize- 1);
                      
        }
        return list;  
    }
    
    public void ListenForOnBeat()
    {
        SpawnAProjectile(1);
        SpawnAProjectile(2);
    }

    public enum AttackMode
    {
        AtoA,
        BtoA
    }
}
