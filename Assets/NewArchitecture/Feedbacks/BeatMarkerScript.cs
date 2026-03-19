using System;
using UnityEngine;

public class BeatMarkerScript : MonoBehaviour
{
   public Transform[] allPoints = new Transform[] { };

   private int currentIndex = 0;
   private float currentPercent = 0;
   public 

   private void FixedUpdate()
   {
      if (currentIndex == 3)
      {
         currentIndex = 0;
         transform.position = allPoints[0].position;
      }
      transform.position = Vector3.Lerp(allPoints[currentIndex].position, allPoints[currentIndex + 1].position, currentPercent);
      currentPercent
   }
}
