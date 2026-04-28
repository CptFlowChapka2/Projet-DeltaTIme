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

    public float playersRotating;

    private void Update()
    {
        rotationPlayer.SetParameter("PlayersRotating", playersRotating);
    }
}
