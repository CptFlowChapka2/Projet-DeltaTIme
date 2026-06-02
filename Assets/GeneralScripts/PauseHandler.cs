using System;
using UnityEngine;
using UnityEngine.Events;

public class PauseHandler : MonoBehaviour
{
   public UnityEvent<PauseState> PauseCalled = new UnityEvent<PauseState>();

   public GameObject PauseObject;
   public GameObject NextButton;
   public GameObject ResumeButton;

   public GameObject WinSceenImage;
   public GameObject LooseSceenImage;
   public GameObject TimeUpSceenImage;
   public GameObject TimeUpScore;
   public GameObject GeneralInfoImage;
   

   private bool hasTerminated;
   public bool hasFinished;
   public bool hasTimeUp;


   private void Start()
   {
      PauseCalled.AddListener(ReceivePause);
      if(hasFinished)NextButton.SetActive(true);
      if(hasTerminated)NextButton.SetActive(false);
   }

   public void ReceivePause(PauseState pState)
   {
      switch (pState)
      {
         case PauseState.lvlFinished:
            hasFinished = true;
            break;
         case PauseState.lvlTerminated:
            hasTerminated = true;
            break;
         case PauseState.lvlTimeUp:
            hasTimeUp = true;
            break;
         case PauseState.noInfo:
            break;
         default:
            throw new ArgumentOutOfRangeException(nameof(pState), pState, null);
      }

      PauseObject.SetActive(!PauseObject.activeSelf);
      if (hasTerminated)
      {
         ResumeButton.SetActive(false);
         GeneralInfoImage.SetActive(false);
         LooseSceenImage.SetActive(true);
      }
      if(hasFinished)
      {
         NextButton.SetActive(true);
         GeneralInfoImage.SetActive(false);
         WinSceenImage.SetActive(true);
         LooseSceenImage.SetActive(false);
      }

      if (hasTimeUp)
      {
         WinSceenImage.SetActive(false);
         LooseSceenImage.SetActive(false);
         TimeUpSceenImage.SetActive(true);
         TimeUpScore.SetActive(true);
      }
   }
   
}

public enum PauseState
{
  noInfo=0,
  lvlTerminated=1,
  lvlFinished=2,
  lvlTimeUp=3,
}
