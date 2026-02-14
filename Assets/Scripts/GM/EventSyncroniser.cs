using System;
using UnityEngine;
using UnityEngine.Events;

public class EventSyncroniser : MonoBehaviour
{
    private UnityEvent<int> playerMoovedOnBeat=new UnityEvent<int>();
    [SerializeField] private float playerCoyoteBeat=0.25f;
    private float player1CoyoteTimer;
    private float player2CoyoteTimer;
    private float beatCoyoteTimer;

    private void Start()
    {
        playerMoovedOnBeat.AddListener(FindAnyObjectByType<CrowdLvl>().ReceivePlayerOnBeat);
    }

    private void Update()
    {
        ReduceCoyote();
    }

    public void ReceivePlayerMoove(int playerI)
    {
        Debug.Log("ReceivedPlayer"+playerI+"Moove");
        
        switch (playerI)
        {
            case 1:
                if (beatCoyoteTimer > 0)
                {
                    playerMoovedOnBeat.Invoke(1);
                    return;
                }
                player1CoyoteTimer = playerCoyoteBeat;
                break;
            case 2:
                if (beatCoyoteTimer > 0)
                {
                    playerMoovedOnBeat.Invoke(2);
                    return;
                }
                player2CoyoteTimer = playerCoyoteBeat;
                break;
        }
    }

    private void ReduceCoyote()
    {
        player1CoyoteTimer -= Time.deltaTime;
        player2CoyoteTimer -= Time.deltaTime;
        beatCoyoteTimer -= Time.deltaTime;
    }
    
    

    public void ReceiveBeat()
    {
        Debug.Log("ReceivedBeat");
        if (player1CoyoteTimer > 0)
        {
            playerMoovedOnBeat.Invoke(1);
            player1CoyoteTimer = 0;
        }
        if (player2CoyoteTimer > 0)
        {
            playerMoovedOnBeat.Invoke(2);
            player2CoyoteTimer = 0;
            
        }

        beatCoyoteTimer = playerCoyoteBeat;
    }
}
