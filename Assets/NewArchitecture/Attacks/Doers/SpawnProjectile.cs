using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class SpawnProjectile : Doer
{
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
        danceFloorSize = attacksManager.gameManager.GetDanceFloorSize();
        allInvalideTileP1 = attacksManager.gameManager.GetDanceFloorAllInvalidTile(1);
        allInvalideTileP2 = attacksManager.gameManager.GetDanceFloorAllInvalidTile(2);
    }

    public override void GetAllUsefulParameters()
    {
        
        onMesureFlag = attacksManager.gameManager.GetOnEndMesure();
        onBeatFlag=attacksManager.gameManager.GetOnBeat();
        
        if (onMesureFlag)
        {
            allInputsThisMeasureP1.Add(attacksManager.gameManager.GetInputsThisMeasure(1));
            allInputsThisMeasureP2.Add(attacksManager.gameManager.GetInputsThisMeasure(2));
        }
    }

    private void FixedUpdate()
    {
        GetAllUsefulParameters();
        if (onBeatFlag)
        {
            SpawnAProjectile(1);
            SpawnAProjectile(2);
        }

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
            2=>allInputsThisMeasureP2

        };
        if (playerInputToProcees.Count==0)return ; 
        Debug.Log("projectile was called to be spawned");
        List<TileId> allInvalidTile = playerID switch
        {
            1=>allInvalideTileP1,
            2=>allInvalideTileP2,
        };
        
        foreach (var inputsThisMeasure in playerInputToProcees)
        {
            if(inputsThisMeasure.Count==0)continue;
           Vector2Int inputsThisMeasureFirst = inputsThisMeasure.First();
            if (inputsThisMeasureFirst == Vector2Int.zero)
            {
                inputsThisMeasure.Remove(inputsThisMeasureFirst);
                continue;
            };
            TileId[] origine=CreateOrigine(inputsThisMeasureFirst, allInvalidTile);
            AssignProjectileId(inputsThisMeasure,origine,playerID);
            inputsThisMeasure.Remove(inputsThisMeasure.First());
        }

    }
    
    private  TileId[] CreateOrigine(Vector2Int inputsThisMesure,List<TileId> invalideTile)
    {
        TileId[] origin = new TileId[] { };
        if (inputsThisMesure == Vector2Int.up)
        {
            var list = invalideTile.FindAll(x => x.position.y == 0);
            list.RemoveAll(x => x.position.x == 0 || x.position.x == danceFloorSize - 1);
            origin = list.ToArray();
        }

        if (inputsThisMesure == Vector2Int.down)
        {
            var list = invalideTile.FindAll(x => x.position.y == danceFloorSize - 1);
            list.RemoveAll(x => x.position.x == 0 || x.position.x == danceFloorSize- 1);
            origin = list.ToArray();
        }

        if (inputsThisMesure == Vector2Int.left)
        {
            var list = invalideTile.FindAll(x => x.position.x == danceFloorSize - 1);
            list.RemoveAll(x => x.position.y == 0 || x.position.y == danceFloorSize - 1);
            origin = list.ToArray();
        }

        if (inputsThisMesure == Vector2Int.right)
        {
            var list = invalideTile.FindAll(x => x.position.x == 0);
            list.RemoveAll(x => x.position.y == 0 || x.position.y == danceFloorSize- 1);
            origin = list.ToArray();
        }
        Debug.Log("origine lenght is "+origin.Length);
        return origin;
    }
    private void AssignProjectileId(List<Vector2Int> inputsThisMesure, TileId[] origin, int playerId)
    {

        Vector2Int inputDirToProcesses = inputsThisMesure.First();
        if (inputDirToProcesses == Vector2Int.zero)
        {
            //todo=feedback
            return;
        }

        int nbrOfSimilareInputInMesure = inputsThisMesure.FindAll(x => x == inputDirToProcesses).Count;
        Debug.Assert(attacksManager!=null,"attacksManager==null");
        bool[] patternToSpawn = attacksManager.GetPatternVariantForAnDir(inputDirToProcesses, nbrOfSimilareInputInMesure);
        for (int i = 0; i < patternToSpawn.Length-1 ; i++)
        {
            if (patternToSpawn[i] is false)
            {
                //il n'y as rien à faire spawn donc on passe à la prochaine case
                continue;
            }
            Debug.Log("what is i ? i is "+i);

            RequestProjectileID(origin[i], inputsThisMesure.First(), playerId);
        }
       
    }
    
    public ProjectileID RequestProjectileID(TileId firstTile , Vector2Int dir,int playerID)
    {
        ProjectileID projectileID = attacksManager.currentlyInactiveProjectileIds.First();
        if (projectileID is null)
        {
            return null;
        }

        attacksManager.SwitchProjectileToActiveList(projectileID);
        projectileID.PutInUse(dir,firstTile,playerID);
        return projectileID;
    }
}
