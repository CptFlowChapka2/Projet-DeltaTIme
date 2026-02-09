using System;
using TMPro;
using UnityEngine;

public class CollectibleCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    public int counter;

    private void Update()
    {
        if (counter < 0)
        {
            counter = 0;
        }
        counterText.text = "Collectibles : " + counter.ToString();
    }
}
