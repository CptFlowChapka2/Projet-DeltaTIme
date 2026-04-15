using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LvlInfos : MonoBehaviour
{
    public float lvlDuration = Mathf.Infinity;
    public float timePerTick = 2f;
    private float timer=0;
    private float currentLvlTime;

    public TextMeshProUGUI timeRemaining;
    public TextMeshProUGUI timeBeforeNextTick;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI totalNbrOfTick;

    public UnityEvent Tick=new UnityEvent();

    [HideInInspector] public int score;
    [HideInInspector] public int tNbrTick;
    private void Update()
    {
        timer += Time.deltaTime;
        currentLvlTime += Time.deltaTime;
        if (timer >= timePerTick)
        {
            tNbrTick += 1;
            timer = 0;
            Tick.Invoke();
        }

        timeBeforeNextTick.text = "next tick in : \n "+(timePerTick - timer).ToString(CultureInfo.InvariantCulture);

        timeRemaining.text ="time in level remaining : \n "+ (lvlDuration - currentLvlTime).ToString(CultureInfo.InvariantCulture);
        
        totalNbrOfTick.text ="Total Nbr Of Tick : \n "+ (tNbrTick).ToString(CultureInfo.InvariantCulture);
        
        scoreText.text ="Score : \n "+ (score).ToString(CultureInfo.InvariantCulture);

        if (currentLvlTime>lvlDuration)
        {
            //todo 
        }
    }
    

  
}
