using System;
using UnityEngine;

public class BeatMarkerScript : MonoBehaviour
{
   public Transform[] allPoints = new Transform[] { };

   public int currentIndex = 0;
   private float currentPercent = 0;
   public float percentByFixedUpdate=0.032f;

   private void FixedUpdate()
   {
      if (currentPercent >= 1)
      {
         currentPercent = 0;
         currentIndex++;
      }
      if (currentIndex == 3)
      {
         currentIndex = 0;
         transform.position = allPoints[0].position;
      }
      
      transform.position = Vector3.Lerp(allPoints[currentIndex].position, allPoints[currentIndex + 1].position, currentPercent);
      currentPercent += percentByFixedUpdate;
      
   }
}
