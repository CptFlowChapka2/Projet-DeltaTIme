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

    public float playersRotating = 0;
    public float numberOfMachinesWorking = 0;
    
    private LvlInfos lvlInfos;

    private void Start()
    {
        lvlInfos = FindAnyObjectByType<LvlInfos>();
        lvlInfos.Tick.AddListener(UpdateOnTick);
    }

    private void Update()
    {
        rotationPlayer.SetParameter("PlayersRotating", playersRotating);
    }
    
    public void UpdateOnTick()
    {
        workingMachines.SetParameter("NumberOfWorkingMachines", numberOfMachinesWorking);
        numberOfMachinesWorking = 0;
    }
    
    
}
