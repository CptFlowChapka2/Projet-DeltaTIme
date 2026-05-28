using System;
using System.Linq;
using UnityEngine;

public class LooseStateHandler : MonoBehaviour
{
    public NeededObject[] CriticalObjects;
    private PauseHandler _pauseHandler;


    private void Update()
    {
        if(CriticalObjects.Any(x=>x.HasALlMissing())) _pauseHandler.PauseCalled.Invoke(PauseState.lvlTerminated);
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

    public bool HasALlMissing()
    {
        return GameObjects.All(go => go == null);
    }

    
}
