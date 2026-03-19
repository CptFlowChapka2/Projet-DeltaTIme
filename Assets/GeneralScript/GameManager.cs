using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onBeginningMeasure = new UnityEvent();
    public UnityEvent onStartCoyoteMeasure = new UnityEvent();
    public UnityEvent onStartCoyoteBeat = new UnityEvent();
    public UnityEvent onMeasure = new UnityEvent();
    public UnityEvent onBeat = new UnityEvent();
    public UnityEvent onEndCoyoteMeasure = new UnityEvent();
    public UnityEvent onEndCoyoteBeat = new UnityEvent();
    
    public PlayerManager player1Manager;
    public PlayerManager player2Manager;
    public MusicManager musicManager;
    public FeedbacksManager feedbacksManager;
    public DanceFloorManager danceFloor1Manager;
    public DanceFloorManager danceFloor2Manager;
    public AttacksManager attacksManager;
    
    private List<Manager> allManagers = new List<Manager>();

    private void Awake()
    {
        GetAllManagers();
        player1Manager.gameManager = this;
        player2Manager.gameManager = this;
        InitializeManager<MusicManager>(out musicManager);
        InitializeManager<FeedbacksManager>(out feedbacksManager);
        danceFloor1Manager.gameManager = this;
        danceFloor2Manager.gameManager = this;
        InitializeManager<AttacksManager>(out attacksManager);
    }

    private void GetAllManagers()
    {
        allManagers = FindObjectsByType<Manager>(FindObjectsSortMode.None).ToList();
    }

    private void InitializeManager<T>(out T appropriateManager,int index=0) where T : Manager
    {
        appropriateManager = (T)allManagers.Where(x => x.GetType() == typeof(T)).ToList()[index];
        appropriateManager.gameManager = this;
    }
    
    //METHODES GETTERS

    public double GetTimePerBeat()
    {
        return 60.0 / musicManager.beatsPerMinute;
    }

    public bool GetInCoyoteBeat()
    {
        return musicManager.inCoyoteBeat;
    }

    public List<Vector2Int> GetInputsThisMeasure(int playerId)
    {
        List<Vector2Int> toReturn = playerId switch
        {
            1 => player1Manager.inputsThisMeasure,
            2 => player2Manager.inputsThisMeasure,

            _ => throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null)
        };
        return toReturn;
    }
    
    public Vector2Int[] GetInputsThisMeasureCache(int playerId)
    {
        Vector2Int[] toReturn = playerId switch
        {
            1 => player1Manager.inputsThisMeasureCache,
            2 => player2Manager.inputsThisMeasureCache,

            _ => throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null)
        };
        return toReturn;
    }

    public int GetNumberOfSuccesses(int playerId)
    {
        int toReturn = playerId switch
        {
            1=>player1Manager.numberOfSuccesses,
            2=>player2Manager.numberOfSuccesses,
            _ => throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null)
        };
        return toReturn;
    }
    
    public List<TileId> GetDanceFloorAllInvalidTile(int playerId)
    {
        List<TileId> toReturn = playerId switch
        {
            1 => danceFloor1Manager.allInvalideTile,
            2 => danceFloor2Manager.allInvalideTile,
            _ => throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null)
        };
        return toReturn;
    }
    
    public int GetDanceFloorSize()
    {
        return danceFloor1Manager.danceFloorSize;
    }

    public GameObject GetPlayerGameObject(int playerId)
    {
        GameObject toReturn = playerId switch
        {
            1=>player1Manager.playerGameObject,
            2=>player2Manager.playerGameObject,
            _ => throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null)
        };
        return toReturn;
    }

    public void DoMovePlayerTeleportToCoord(Vector2Int coords,int playerId)
    {
        switch (playerId)
        {
            case 1 :
                player1Manager.DoTeleportPlayerToCoords(coords);
                break;
            case 2 :
                player2Manager.DoTeleportPlayerToCoords(coords);
                break;
        }
    }

    public Vector2Int GetPlayerCurrentRelativeCoords(int playerId)
    {
        Vector2Int toReturn = playerId switch
        {
            1=>player1Manager.currentPlayerCoords,
            2=>player2Manager.currentPlayerCoords,
            _ => throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null)
        };
        return toReturn;
        
    }

    public void DoCallAllManagerOfTypeToForceGetUsefullData<Tm,Td>() where Tm: Manager where Td:Doer
    {
        if (typeof(Tm).IsSubclassOf(typeof(Manager)))//si on spécifie le type de manager alors on cherche celui la
        {
            
            allManagers.Where(x=> x.GetType() == typeof(Tm)).ToList().ForEach(x=>x.ForceDoerToGetUsefullData<Td>());
            return;
        }
        //sinon on le fait pour tout le monde 
        allManagers.ForEach(x=>x.ForceDoerToGetUsefullData<Doer>());//on met Doer ici car aucun manager ne partage de Doer
    }
    public void DoCallAllManagerOfTypeToForceSetUsefullData<Tm,Td>() where Tm: Manager where Td:Doer
    {
        if (typeof(Tm).IsSubclassOf(typeof(Manager)))//si on spécifie le type de manager alors on cherche celui la
        {
            
            allManagers.Where(x=> x.GetType() == typeof(Tm)).ToList().ForEach(x=>x.ForceDoerToSetUsefullData<Td>());
            return;
        }
        //sinon on le fait pour tout le monde 
        allManagers.ForEach(x=>x.ForceDoerToSetUsefullData<Doer>());//on met Doer ici car aucun manager ne partage de Doer
    }

    public TileId[,] GetAllTileid(int playerId)
    {
        switch (playerId)
        {
            case 1 :
                 return danceFloor1Manager.allTileID;
            case 2 :
                return danceFloor2Manager.allTileID;
            default:
                throw new ArgumentOutOfRangeException(nameof(playerId)+" was called with invalide int= "+playerId);
        }
    }

    public void DoTileListModification(Vector2Int tileToModify, ProjectileID toDo,
       tileIdOrder thingToDo,int playerId,bool overwriteToAll=false)
    {
        if (overwriteToAll)
        {
            
            danceFloor1Manager.DoModifyTileList(tileToModify, toDo, thingToDo, overwriteToAll);
            danceFloor2Manager.DoModifyTileList(tileToModify, toDo, thingToDo, overwriteToAll);
        }
        
        switch (playerId)
        {
            case 1:
                danceFloor1Manager.DoModifyTileList(tileToModify, toDo, thingToDo);
                break;
            case 2:
                danceFloor2Manager.DoModifyTileList(tileToModify, toDo, thingToDo);
                break;
        }
    }

    public TileId[,] GetAllTileArray(int playerId)
    {
        TileId[,] toReturn = playerId switch
        {
            1=>danceFloor1Manager.allTileID,
            2=>danceFloor2Manager.allTileID,
            _ => throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null)
        };
        return toReturn;
        
    }

    public void DoCallSpecificFeedback(FeedbackCaller feedback)
    {
        feedbacksManager.Call(feedback);
    }

    public int GetNumberOfConsecutiveUntouchedMeasures(int i)
    {
        if (i == 1)
        {
            return player1Manager.numberOfConsecutivePerfectMeasures;
        }
        else if (i == 2)
        {
            return player2Manager.numberOfConsecutivePerfectMeasures;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(i));
        }
    }

    public bool GetIsAlreadyHit(int playerId)
    {
        if (playerId == 1)
        {
            return player1Manager.isAlreadyHit;
        }
        else
        {
            return player2Manager.isAlreadyHit;
        }
    }
}
