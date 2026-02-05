using System;
using System.Collections.Generic;
using UnityEngine;

public class BeatEnabler : MonoBehaviour
{
    private MusicParameters musicParameters;
    private GameObject player;
    private PlayerMovement playerMovement;
    [SerializeField] private float timingWindow;
    [SerializeField] private Color onBeatColor;
    [SerializeField] private Color errorColor;
    public bool onBeat = false;
    public List<int> currentPattern;
    public int measureCounter = 1;
    private Color originalColor;
    private float t;

    private void Start()
    {
        musicParameters = GetComponent<MusicParameters>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        originalColor = player.GetComponent<MeshRenderer>().material.color;
        currentPattern = new List<int>(musicParameters.beatPattern1);
    }

    private void Update()
    {
        t += Time.deltaTime;
        ActivateBeatOnPattern();

        if (onBeat && !playerMovement.hasMadeAnError)
        {
            player.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            player.GetComponent<MeshRenderer>().material.color = onBeatColor;
        }
        else if (playerMovement.hasMadeAnError)
        {
            player.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            player.GetComponent<MeshRenderer>().material.color = errorColor;
        }
        else
        {
            player.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            player.GetComponent<MeshRenderer>().material.color = originalColor;
        }
    }

    private void ActivateBeatOnPattern()
    {
        //Quand une mesure est terminée
        if (t >= (60 * musicParameters.beatsPerMesure / musicParameters.beatsPerMinute))
        {
            t = 0;
            measureCounter++;
            if (measureCounter < musicParameters.patternsProgression.Length)
            {
                switch (musicParameters.patternsProgression[measureCounter])
                {
                    case 1:
                        currentPattern = new List<int>(musicParameters.beatPattern1);
                        break;
                    case 2:
                        currentPattern = new List<int>(musicParameters.beatPattern2);
                        break;
                    case 3:
                        currentPattern = new List<int>(musicParameters.beatPattern3);
                        break;
                    default:
                        break;
                }
            }
        }
        //Juste avant qu'une mesure soit terminée
        else if (t >= (60 * musicParameters.beatsPerMesure / musicParameters.beatsPerMinute) - timingWindow * 1.5f)
        {
            onBeat = true;
        }
        
        else if (currentPattern.Count > 0)
        {
            //Quand la fenêtre de timing d'un beat se referme
            if (t >= ((60 * currentPattern[0]) / (musicParameters.beatsPerMinute * musicParameters.subdivMaxPerBeat) +
                      timingWindow * 1.5f))
            {
                onBeat = false;
                currentPattern.RemoveAt(0);
                playerMovement.alreadyMovedThisBeat = false;
                if (playerMovement.hasMadeAnError)
                {
                    playerMovement.beatsSinceError++;
                }
            }
            //Quand la fenêtre de timing d'un beat s'ouvre
            else if (t >= ((60 * currentPattern[0]) / (musicParameters.beatsPerMinute * musicParameters.subdivMaxPerBeat) -
                       timingWindow * 1.5f))
            {
                onBeat = true;
            }
        }
    }
}
