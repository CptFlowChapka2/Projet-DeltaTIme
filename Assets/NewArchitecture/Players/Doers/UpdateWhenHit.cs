using System;
using UnityEngine;

public class UpdateWhenHit : Doer
{
    private PlayerManager playerManager;
    
    private TileId currentTileId;
    private bool isAlreadyHit = false;

    private double invincibilityTime;
    private double t;

    private void Start()
    {
        playerManager = (PlayerManager)manager;
    }

    private void Update()
    {
        GetAllUsefulParameters();
        VerifyIfPlayerIsHit();
        SetAllUsedParameters();
    }

    private void VerifyIfPlayerIsHit()
    {
        if (isAlreadyHit)
        {
            t += Time.deltaTime;
            if (t >= invincibilityTime)
            {
                t = 0;
                isAlreadyHit = false;
            }
        }
        else if (currentTileId.allProjectileOnThisTile.Count > 0 && !isAlreadyHit)
        {
            isAlreadyHit = true;
            playerManager.gameManager.DoCallSpecificFeedback(playerManager.gameManager.feedbacksManager.takingHitFeedback);
        }
    }

    public override void GetAllUsefulParameters()
    {
        currentTileId = playerManager.currentTileId;
        isAlreadyHit = playerManager.isAlreadyHit;
        invincibilityTime = playerManager.gameManager.GetTimePerBeat();
    }

    public override void SetAllUsedParameters()
    {
        playerManager.isAlreadyHit = isAlreadyHit;
    }
}
