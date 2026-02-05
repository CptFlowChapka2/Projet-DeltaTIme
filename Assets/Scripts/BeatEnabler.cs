using System;
using System.Collections.Generic;
using UnityEngine;

public class BeatEnabler : MonoBehaviour
{
    private MusicParameters musicParameters;
    private GameObject player;
    [SerializeField] private float timingWindow;
    [SerializeField] private Color onBeatColor;
    public bool onBeat = false;
    public List<int> currentPattern;
    public int measureCounter = 1;
    private Color originalColor;
    private Color currentColor;
    private float t;

    private void Start()
    {
        musicParameters = GetComponent<MusicParameters>();
        player = GameObject.FindGameObjectWithTag("Player");
        originalColor = player.GetComponent<Renderer>().material.color;
        currentColor = player.GetComponent<Renderer>().material.color;
        currentPattern = new List<int>(musicParameters.beatPattern1);
    }

    private void Update()
    {
        t += Time.deltaTime;
        ActivateBeatOnPattern();

        if (onBeat)
        {
            player.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            currentColor = onBeatColor;
        }
        else
        {
            player.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            currentColor = originalColor;
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
        else if (t >= (60 * musicParameters.beatsPerMesure / musicParameters.beatsPerMinute) - timingWindow)
        {
            onBeat = true;
        }
        
        else if (currentPattern.Count > 0)
        {
            //Quand la fenêtre de timing d'un beat se referme
            if (t >= ((60 * currentPattern[0]) / (musicParameters.beatsPerMinute * musicParameters.subdivMaxPerBeat) +
                      timingWindow))
            {
                onBeat = false;
                currentPattern.RemoveAt(0);
            }
            //Quand la fenêtre de timing d'un beat s'ouvre
            else if (t >= ((60 * currentPattern[0]) / (musicParameters.beatsPerMinute * musicParameters.subdivMaxPerBeat) -
                       timingWindow))
            {
                onBeat = true;
            }
        }
    }
}
