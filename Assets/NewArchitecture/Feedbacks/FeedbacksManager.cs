using System;
using FMODUnity;
using UnityEngine;

public enum PlayerState
{
    Safe,
    Damaged,
    Undefined
}

public class FeedbacksManager : Manager
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private StudioEventEmitter hitSound;
    [SerializeField] private StudioEventEmitter dingSound;
    private InputTrackerAndPatternSpawner player1Inputs;
    private InputTrackerAndPatternSpawner player2Inputs;
    public float actualP1Level = 0f;
    public float actualP2Level = 0f;
    public float numberOfMeasuresP1Untouched = 0f;
    public float numberOfMeasuresP2Untouched = 0f;

    private void Start()
    {
        player1Inputs = player1.GetComponent<InputTrackerAndPatternSpawner>();
        player2Inputs = player2.GetComponent<InputTrackerAndPatternSpawner>();
    }

    public void UpdateLevels()
    {
        if (player1Inputs.numberOfSuccesses > actualP1Level)
        {
            actualP1Level++;
        }
        else if (player1Inputs.numberOfSuccesses < actualP1Level)
        {
            actualP1Level--;
        }
        
        if (player2Inputs.numberOfSuccesses > actualP2Level)
        {
            actualP2Level++;
        }
        else if (player2Inputs.numberOfSuccesses < actualP2Level)
        {
            actualP2Level--;
        }
    }
    
    public void DamageFeedback(GameObject player)
    {
        //todo : Jouer le son de dégâts + animer/changer couleur du joueur en question
        hitSound.Play();
    }

    public void CorrectMoveFeedback()
    {
        dingSound.Play();
    }

    public void CrowdUpdatingFeedback(GameObject player, bool touched)
    {
        int valueUpdate = 0;
        if (touched)
        {
            valueUpdate = -1;
        }
        else
        {
            valueUpdate = 1;
        }
        
        player.GetComponent<LinkedCrowd>().linkedCrowd.TryChangeLvl(valueUpdate);
    }
}
