using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EventSyncroniser : MonoBehaviour
{
    private UnityEvent<int, bool,Vector2Int> playerMoovedOnBeat = new UnityEvent<int, bool,Vector2Int>();
    [SerializeField] private float playerCoyoteBeat=0.25f;
    

    private bool inCoyote = false;

    private void Start()
    {
        playerMoovedOnBeat.AddListener(FindAnyObjectByType<CrowdLvl>().ReceivePlayerOnBeat);
        FindObjectsByType<NewPatternSpawner>(FindObjectsInactive.Exclude,FindObjectsSortMode.InstanceID).ToList().ForEach(
            x=>playerMoovedOnBeat.AddListener(x.ReceivePlayerInput)
            );
    }

    public void ReceivePlayerMoove(int playerI,Vector2Int movement)
    {
        
        
        playerMoovedOnBeat.Invoke(playerI,inCoyote,movement);
        
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
