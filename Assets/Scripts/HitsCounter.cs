using System;
using TMPro;
using UnityEngine;

public class HitsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text hitsCounterText;
    public int hitsCounter;
    public int numberOfCollectiblesLostPerHit;

    private void Update()
    {
        hitsCounterText.text = "Hits : " + hitsCounter.ToString();
    }
}
