using System;
using UnityEngine;

public class NavScreenChanger : MonoBehaviour
{
    [SerializeField] private PauseHandler _pauseHandler;
    [SerializeField] private GameObject generalInfos;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private bool lockedInEndState;

    private void Update()
    {
        if (!_pauseHandler.levelHasOfficiallyEnded || lockedInEndState) return;
        generalInfos.SetActive(false);
        if (_pauseHandler.hasWon)
        {
            winScreen.SetActive(true);
            lockedInEndState = true;
        }
        else
        {
            loseScreen.SetActive(true);
            lockedInEndState = false;
        }
    }
}
