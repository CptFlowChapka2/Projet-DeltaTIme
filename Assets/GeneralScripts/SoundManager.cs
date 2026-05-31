using System;
using FMODUnity;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public StudioEventEmitter grabRelease;
    public StudioEventEmitter rotationPlayer;
    public StudioEventEmitter tick;
    public StudioEventEmitter collision;
    public StudioEventEmitter finishRecipe;
    public StudioEventEmitter workingMachines;
    public StudioEventEmitter clic;
    public StudioEventEmitter extensionPlayer;
    public StudioEventEmitter lacMagiqueProd;
    public StudioEventEmitter foretProd;
    public StudioEventEmitter scoring;
    public StudioEventEmitter win;
    public StudioEventEmitter lose;

    public float playersRotating = 0;
    public float playersExtending = 0;
    public float numberOfMachinesWorking = 0;
    public float lakeTilesActivated = 0;
    public float forestTilesActivated = 0;
    
    
    private LvlInfos lvlInfos;

    private void Start()
    {
        lvlInfos = FindAnyObjectByType<LvlInfos>();
        lvlInfos.Tick.AddListener(UpdateOnTick);
    }

    private void Update()
    {
        rotationPlayer.SetParameter("PlayersRotating", playersRotating);
        extensionPlayer.SetParameter("PlayersExtending", playersExtending);
    }
    
    public void UpdateOnTick()
    {
        workingMachines.SetParameter("NumberOfWorkingMachines", numberOfMachinesWorking);
        lacMagiqueProd.SetParameter("LakeTilesActivated", lakeTilesActivated);
        foretProd.SetParameter("ForestTilesActivated", forestTilesActivated);
        numberOfMachinesWorking = 0;
        lakeTilesActivated = 0;
        forestTilesActivated = 0;
    }
    
    
}
