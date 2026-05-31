using System;
using UnityEngine;
using UnityEngine.Events;

public class PauseHandler : MonoBehaviour
{
   public UnityEvent<PauseState> PauseCalled = new UnityEvent<PauseState>();

   public GameObject PauseObject;
   public GameObject NextButton;
   public GameObject ResumeButton;
   public GameObject BackgroundImage;

   public bool levelHasOfficiallyEnded;
   public bool hasWon;


   private void Start()
   {
      PauseCalled.AddListener(ReceivePause);
      if(levelHasOfficiallyEnded)NextButton.SetActive(false);
      if(hasWon)NextButton.SetActive(true);
   }

   public void ReceivePause(PauseState pState)
   {
      if (pState == PauseState.lvlFinished) levelHasOfficiallyEnded = true;
      if (pState == PauseState.lvlTerminated) levelHasOfficiallyEnded = true;
      PauseObject.SetActive(!PauseObject.activeSelf);
      BackgroundImage.SetActive(!BackgroundImage.activeSelf);
      if(levelHasOfficiallyEnded)NextButton.SetActive(false);
      if(hasWon)NextButton.SetActive(true);
   }
   
}

public enum PauseState
{
  noInfo=0,
  lvlTerminated=1,
  lvlFinished=2,
}
