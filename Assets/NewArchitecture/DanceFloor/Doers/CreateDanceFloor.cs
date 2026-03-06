using System;
using UnityEngine;

public class CreateDanceFloor : Doer
{
    private DanceFloorManager danceFloorManager;

    private void Start()
    {
        danceFloorManager = (DanceFloorManager)manager;
    }
    
    
}
