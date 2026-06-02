using System;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LooseStateHandler : MonoBehaviour
{
    public NeededObject[] CriticalObjects;
    private PauseHandler _pauseHandler;
    private bool criticalSituationReached = false;


    private void Update()
    {
        if (CriticalObjects.Any(x => x.IsInvalide()) && !criticalSituationReached)
        {
            _pauseHandler.PauseCalled.Invoke(PauseState.lvlTerminated);
            criticalSituationReached = true;
        }
    }

    private void Start()
    {
        _pauseHandler = GetComponent<PauseHandler>();

        var foundPlayer = FindObjectsByType<Player>(FindObjectsSortMode.None);

        for (int i = 0; i < foundPlayer.Length; i++)
        {
            CriticalObjects[0].GameObjects[i] = foundPlayer[i].gameObject;
        }
        
    }
}

[Serializable]
public struct NeededObject
{
    public GameObject[] GameObjects;

    public bool allOrAny;
    
    public bool IsInvalide()
    {
        return allOrAny switch {
            true => HasALlMissing(),
            false => HasANyMissing()
        };
    }
    
    private bool HasALlMissing()
    {
        return GameObjects.All(go => go == null);
    }

    private bool HasANyMissing()
    {
        return GameObjects.Any(go => go == null);
    }
    
}
