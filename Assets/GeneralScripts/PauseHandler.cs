using System;
using UnityEngine;
using UnityEngine.Events;

public class PauseHandler : MonoBehaviour
{
   public UnityEvent<PauseState> PauseCalled = new UnityEvent<PauseState>();

   public GameObject PauseObject;
   public GameObject NextButton;
   public GameObject ResumeButton;

   private bool hasTerminated;
   private bool hasFinished;


   private void Start()
   {
      PauseCalled.AddListener(ReceivePause);
   }

   public void ReceivePause(PauseState pState)
   {
      if (pState == PauseState.lvlFinished) hasTerminated = true;
      if (pState == PauseState.lvlTerminated) hasTerminated = true;
      PauseObject.SetActive(!PauseObject.activeSelf);
      if(hasFinished)NextButton.SetActive(true);
      if(hasTerminated)NextButton.SetActive(false);
   }
   
}

public enum PauseState
{
  noInfo=0,
  lvlTerminated=1,
  lvlFinished=2,
}
