using System;
using UnityEngine;
using UnityEngine.Events;

public class DamagingTileScript : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;
    private NewTileScript newTileScript;
    private BeatClock beatClock;
    private FeedbacksManager feedbacksManager;
    public bool applyDamages = false;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        newTileScript = GetComponent<NewTileScript>();
        beatClock = FindFirstObjectByType<BeatClock>();
        feedbacksManager = FindFirstObjectByType<FeedbacksManager>();
        beatClock.onBeat.AddListener(SwitchLethalityOff);
        beatClock.onEndBeat.AddListener(SwitchLethalityOn);
    }

    private void Update()
    {
        if (applyDamages)
        {
            VerifyHit(player1);
            VerifyHit(player2);
        }
    }

    public void SwitchLethalityOn()
    {
        if (newTileScript.thisState == NewTileScript.TileState.Damaging)
        {
            applyDamages = true;
        }
    }
    
    public void SwitchLethalityOff()
    {
        if (newTileScript.thisState == NewTileScript.TileState.Damaging)
        {
            applyDamages = false;
        }
    }

    private void VerifyHit(GameObject player)
    {
        if (new Vector3(player.transform.position.x, 0, player.transform.position.z) == new Vector3(transform.position.x, 0, transform.position.z))
        {
            HealthSystem healthSystem = player.GetComponent<HealthSystem>();
            if (!healthSystem.isInvincible)
            {
                healthSystem.numberOfTimeTouched++;
                feedbacksManager.DamageFeedback(player.gameObject);
                feedbacksManager.CrowdUpdatingFeedback(player.gameObject, true);
                healthSystem.isInvincible = true;
            }
        }
    }
}
