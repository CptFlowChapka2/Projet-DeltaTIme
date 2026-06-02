using System;
using FMODUnity;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private bool isInMenu = false;
    
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
    public StudioEventEmitter marginStart;

    public float playersRotating = 0;
    public float playersExtending = 0;
    public float playersExtendingBuffer = 0;
    public float numberOfMachinesWorking = 0;
    public float lakeTilesActivated = 0;
    public float forestTilesActivated = 0;
    
    
    private LvlInfos lvlInfos;

    private void Start()
    {
        if (isInMenu) return;
        lvlInfos = FindAnyObjectByType<LvlInfos>();
        lvlInfos.Tick.AddListener(UpdateOnTick);
    }

    private void Update()
    {
        if (isInMenu) return;
        rotationPlayer.SetParameter("PlayersRotating", playersRotating);

        float newExtendingBuffer = Mathf.MoveTowards(playersExtendingBuffer,
            playersExtending, Mathf.Clamp((playersExtending - playersExtendingBuffer) * 0.1f, 0.05f, Mathf.Infinity));
        extensionPlayer.SetParameter("PlayersExtending",newExtendingBuffer );
        playersExtendingBuffer = newExtendingBuffer;
    }
    
    public void UpdateOnTick()
    {
        if (isInMenu) return;
        workingMachines.SetParameter("NumberOfWorkingMachines", numberOfMachinesWorking);
        lacMagiqueProd.SetParameter("LakeTilesActivated", lakeTilesActivated);
        foretProd.SetParameter("ForestTilesActivated", forestTilesActivated);
        numberOfMachinesWorking = 0;
        lakeTilesActivated = 0;
        forestTilesActivated = 0;
    }
    
    
}
