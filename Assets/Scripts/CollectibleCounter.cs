using System;
using TMPro;
using UnityEngine;

public class CollectibleCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    public int counter;

    private void Update()
    {
        counterText.text = "Collectibles : " + counter.ToString();
    }
}
