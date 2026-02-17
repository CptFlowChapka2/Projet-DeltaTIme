using System;
using UnityEngine;
using UnityEngine.Events;

public class EventSyncroniser : MonoBehaviour
{
    private UnityEvent<int, bool> playerMoovedOnBeat = new UnityEvent<int, bool>();
    [SerializeField] private float playerCoyoteBeat=0.25f;
    

    private bool inCoyote = false;

    private void Start()
    {
        playerMoovedOnBeat.AddListener(FindAnyObjectByType<CrowdLvl>().ReceivePlayerOnBeat);
    }

    public void ReceivePlayerMoove(int playerI)
    {
        playerMoovedOnBeat.Invoke(playerI,inCoyote);
        
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
