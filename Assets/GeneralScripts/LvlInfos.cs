using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LvlInfos : MonoBehaviour
{
    private SoundManager soundManager;
    public float lvlDuration = Mathf.Infinity;
    public float lvlDurationWhenRush = 180f;
    public float timePerTick = 2f;
    [HideInInspector] public float timer=0;
    private float currentLvlTime;
    private bool alreadyPlayedSound = false;

    public TextMeshProUGUI timeRemaining;
    public TextMeshProUGUI timeBeforeNextTick;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI totalNbrOfTick;

    public UnityEvent Tick = new UnityEvent();

    public int score;
    public int neededScore=-1;
    [HideInInspector] public int tNbrTick;

    [SerializeField] public bool validIngredientHasSpawned = false;
    [SerializeField] private IngredientType validIngredientType;
   

    private PauseHandler _pauseHandler;

    private float timeMod = 1f;

    private bool hasFinishedMinimumScore=false;
    private bool hasSurpassMaxTime=false;

    public Text timeUpScore;
    
   

    private void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
        _pauseHandler = GetComponent<PauseHandler>();
        _pauseHandler.PauseCalled.AddListener(ReceivePause);
        timeMod = 0f;
    }
    

    private void Update()
    {
        float timeModer = timeMod * Time.deltaTime;
        
        timer +=timeModer;
        currentLvlTime += timeModer;
        if(timeModer==0) return;
        if (timer >= timePerTick - 0.2f && !alreadyPlayedSound)
        {
            alreadyPlayedSound = true;
            soundManager.tick.Play();
        }
        if (timer >= timePerTick)
        {
            alreadyPlayedSound = false;
            tNbrTick += 1;
            timer = 0;
            Tick.Invoke();
        }

        VerifyWinCon();
        VerifyOvertime();
    }

    private void VerifyOvertime()
    {
        if (!hasSurpassMaxTime && currentLvlTime > lvlDuration) //overtime
        {
            hasSurpassMaxTime = true;
            if (hasFinishedMinimumScore) //mode infini
            {
                timeUpScore.text = "SCORE : " + score.ToString() ;
                lvlDuration = Mathf.Infinity;
                _pauseHandler.PauseCalled.Invoke(PauseState.lvlTimeUp);
            }
            else //lose
            {
                soundManager.lose.Play();
                _pauseHandler.PauseCalled.Invoke(PauseState.lvlTerminated);
            }
        }
    }

    private void VerifyWinCon()
    {
        if (!hasFinishedMinimumScore && neededScore != -1 && score >= neededScore) //win
        {
            soundManager.win.Play();
            hasFinishedMinimumScore = true;
            _pauseHandler.PauseCalled.Invoke(PauseState.lvlFinished);
        }
    }

    [Obsolete] private void UpdateGeneralInformationText()
    {
        timeBeforeNextTick.text = "next tick in : \n "+(timePerTick - timer).ToString(CultureInfo.InvariantCulture);
        
        timeRemaining.text ="time in level remaining : \n "+ (lvlDuration - currentLvlTime).ToString(CultureInfo.InvariantCulture);
        
        totalNbrOfTick.text ="Total Nbr Of Tick : \n "+ (tNbrTick).ToString(CultureInfo.InvariantCulture);
        
       
        if (neededScore != -1)
        {
            scoreText.text ="Score : \n "+ (score).ToString(CultureInfo.InvariantCulture)+"/"+(neededScore).ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            scoreText.text ="Score : \n "+ (score).ToString(CultureInfo.InvariantCulture);
        }
        

        
    }

    public void CheckForValidIngredient(IngredientType spawnedIngredient)
    {
        if (!validIngredientHasSpawned && validIngredientType == spawnedIngredient) //le premier objet de score apparait
        {
            soundManager.marginStart.Play();
            validIngredientHasSpawned = true;
            currentLvlTime = 0;
            lvlDuration = lvlDurationWhenRush;
        }
    }

    public void ReceivePause(PauseState pState)
    {
        timeMod = timeMod switch
        {
            1=>0,
            0=>1,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
