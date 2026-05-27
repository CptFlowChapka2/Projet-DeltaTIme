using System;
using UnityEngine;
using UnityEngine.Events;

public class PauseHandler : MonoBehaviour
{
   public UnityEvent PauseCalled = new UnityEvent();

   public GameObject PauseObject;


   private void Start()
   {
      PauseCalled.AddListener(ReceivePause);
   }

   public void ReceivePause()
   {
      PauseObject.SetActive(!PauseObject.activeSelf);
   }
   
}
