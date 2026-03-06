using System;
using UnityEngine;

public class TouchZone : MonoBehaviour
{
    private OldBeatClock _oldBeatClock;
    private HealthSystem player1;
    private HealthSystem player2;
    private bool alreadyTouched = false;

    private void Start()
    {
        _oldBeatClock = GameObject.FindGameObjectWithTag("GM").GetComponent<OldBeatClock>();
        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (transform.position != new Vector3(player1.transform.position.x,
                transform.position.y,
                player1.transform.position.z) && 
            transform.position != new Vector3 (player2.transform.position.x,
                transform.position.y,
                player2.transform.position.z))
        {
            alreadyTouched = false;
        }
        
        if (alreadyTouched) return;
        
        if (transform.position == new Vector3(player1.transform.position.x,
                transform.position.y,
                player1.transform.position.z))
        {
            alreadyTouched = true;
            player1.numberOfTimeTouched++;
        }
        else if (transform.position == new Vector3(player2.transform.position.x,
                     transform.position.y,
                     player2.transform.position.z))
        {
            alreadyTouched = true;
            player2.numberOfTimeTouched++;
        }
    }
}
