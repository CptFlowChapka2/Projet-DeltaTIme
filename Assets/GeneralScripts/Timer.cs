using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float lvlDuration = Mathf.Infinity;
    public float timePerTick = 2f;
    private float timer=0;
    private float currentLvlTime;

    public UnityEvent Tick=new UnityEvent();
    private void Update()
    {
        timer += Time.deltaTime;
        currentLvlTime += Time.deltaTime;
        if (timer >= timePerTick)
        {
            timer = 0;
            Tick.Invoke();
        }

        if (currentLvlTime>lvlDuration)
        {
            //todo 
        }
    }

  
}
