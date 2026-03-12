using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        InitializeManager<PlayerManager>(out player1Manager, 0);
        player1Manager.playerId = 1;
        InitializeManager<PlayerManager>(out player2Manager, 1);
        player2Manager.playerId = 2;
        InitializeManager<MusicManager>(out musicManager);
        InitializeManager<FeedbacksManager>(out feedbacksManager);
        InitializeManager<DanceFloorManager>(out danceFloor1Manager,0);
        danceFloor1Manager.associatedPlayerId = 1;
        InitializeManager<DanceFloorManager>(out danceFloor2Manager,1);
        danceFloor2Manager.associatedPlayerId = 2;
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

    public bool GetInCoyoteBeat()
    {
        return musicManager.inCoyoteBeat;
    }
    
    public bool GetOnEndCoyoteBeat()
    {
        return musicManager.onEndCoyoteBeat;
    }

    public bool GetOnStartCoyoteMeasure()
    {
        return musicManager.onStartCoyoteMeasure;
    }

    public bool GetOnBeat()
    {
        return musicManager.onBeat;
    }

    public bool GetOnEndMesure()
    {
        return musicManager.onEndCoyoteMeasure;
    }

    public List<Vector2Int> GetInputsThisMeasure(int playerId)
    {
        List<Vector2Int> toReturn = playerId switch
        {
            1=> player1Manager.inputsThisMeasure,
            2=>player2Manager.inputsThisMeasure,

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
            1=> danceFloor1Manager.allInvalideTile,
            2=>danceFloor2Manager.allInvalideTile,

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
            2=>danceFloor2Manager.allTileID

        };
        return toReturn;
        
    }

    public void DoCallSpecificFeedback(FeedbackCaller feedback)
    {
        feedbacksManager.Call(feedback);
    }
}
