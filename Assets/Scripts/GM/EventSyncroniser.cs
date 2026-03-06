using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EventSyncroniser : MonoBehaviour
{
    public UnityEvent<int, bool,Vector2Int> playerMovedOnBeat = new UnityEvent<int, bool,Vector2Int>();
    [SerializeField] private float playerCoyoteBeat=0.25f;
    
    private bool inCoyote = false;

    private void Start()
    {
        playerMovedOnBeat.AddListener(FindAnyObjectByType<CrowdLvl>().ReceivePlayerOnBeat);
        FindObjectsByType<InputTrackerAndPatternSpawner>(FindObjectsInactive.Exclude,FindObjectsSortMode.InstanceID).ToList().ForEach(
            x=>playerMovedOnBeat.AddListener(x.ReceivePlayerInput)
            );//
    }

    public void ReceivePlayerMove(int playerI,Vector2Int movement)
    {
        playerMovedOnBeat.Invoke(playerI,inCoyote,movement);
    }
    

    public void ReceiveStartCoyote()
    {
        inCoyote = true;
    }
    
    public void ReceiveEndCoyote()
    {
        inCoyote = false;
    }
}
