using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LvlInfos : MonoBehaviour
{
    private SoundManager soundManager;
    public float lvlDuration = Mathf.Infinity;
    public float lvlDurationWhenRush = 180f;
    public float timePerTick = 2f;
    private float timer=0;
    private float currentLvlTime;
    private bool alreadyPlayedSound = false;

    public TextMeshProUGUI timeRemaining;
    public TextMeshProUGUI timeBeforeNextTick;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI totalNbrOfTick;

    public UnityEvent Tick = new UnityEvent();

    [HideInInspector] public int score;
     public int neededscore=-1;
    [HideInInspector] public int tNbrTick;

    [SerializeField] private bool validIngredientHasSpwaned = false;
    [SerializeField] private IngredientType validIngredientType;
    private Player[] _players;

   

    private void Start()
    {
        _players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    private void Update()
    {
        
        timer += Time.deltaTime;
        currentLvlTime += Time.deltaTime;
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

        UpdateGeneralInformationText();
        
        if (currentLvlTime>lvlDuration||(neededscore!=-1&&score>=neededscore))
        {
            _players[0].PauseMenu.SetActive(true);
            foreach (Player player in _players)
            {
                player.isInPause = true;
                player.playerInput.currentActionMap = player.UIInputActionMap;
            }
        }
    }

    private void UpdateGeneralInformationText()
    {
        timeBeforeNextTick.text = "next tick in : \n "+(timePerTick - timer).ToString(CultureInfo.InvariantCulture);

        timeRemaining.text ="time in level remaining : \n "+ (lvlDuration - currentLvlTime).ToString(CultureInfo.InvariantCulture);
        
        totalNbrOfTick.text ="Total Nbr Of Tick : \n "+ (tNbrTick).ToString(CultureInfo.InvariantCulture);
        
       
        if (neededscore != -1)
        {
            scoreText.text ="Score : \n "+ (score).ToString(CultureInfo.InvariantCulture)+"/"+(neededscore).ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            scoreText.text ="Score : \n "+ (score).ToString(CultureInfo.InvariantCulture);
        }
        

        
    }

    public void CheckForValidIngredient(IngredientType spawnedIngredient)
    {

        if (validIngredientHasSpwaned != (validIngredientType == spawnedIngredient))
        {
            validIngredientHasSpwaned = true;
            currentLvlTime = 0;
            lvlDuration = lvlDurationWhenRush;
        }
    }
}
