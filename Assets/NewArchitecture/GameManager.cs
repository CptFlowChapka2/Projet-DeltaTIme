using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player1Manager;
    [SerializeField] private PlayerManager player2Manager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private FeedbacksManager feedbacksManager;
    [SerializeField] private DanceFloorManager danceFloorManager;
    [SerializeField] private AttacksManager attacksManager;
    
    private List<Manager> allManagers = new List<Manager>();

    private void Awake()
    {
        GetAllManagers();
        InitializeManager<PlayerManager>(out player1Manager, 0);
        InitializeManager<PlayerManager>(out player2Manager, 1);
        InitializeManager<MusicManager>(out musicManager);
        InitializeManager<FeedbacksManager>(out feedbacksManager);
        InitializeManager<DanceFloorManager>(out danceFloorManager);
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
}
