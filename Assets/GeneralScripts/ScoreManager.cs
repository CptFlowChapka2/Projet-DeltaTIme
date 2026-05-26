using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Image[] scoreImages;
    [SerializeField] private TextMeshProUGUI excedentaryScore;
    [SerializeField] private LvlInfos lvlInfos;
    [SerializeField] private float scoreIconOffset;

    private int currentScore = 0;
    private int excedentaryScoreCounter = 0;

    private void Start()
    {
        for (int i = 0; i < scoreImages.Length; i++)
        {
            if (i >= lvlInfos.neededScore)
            {
                scoreImages[i].gameObject.SetActive(false);
            }
        }

        Vector3 textPosition = new Vector3(excedentaryScore.transform.position.x - ((10 - lvlInfos.neededScore) * scoreIconOffset), excedentaryScore.transform.position.y, excedentaryScore.transform.position.z);
        excedentaryScore.transform.position = textPosition;
        excedentaryScore.enabled = false;
    }

    private void Update()
    {
        if (lvlInfos.score == currentScore) return;
        if (lvlInfos.score > lvlInfos.neededScore)
        {
            excedentaryScoreCounter = lvlInfos.score - lvlInfos.neededScore;
            if (currentScore >= lvlInfos.neededScore) return;
            foreach (Image scoreImage in scoreImages)
            {
                scoreImage.color = Color.white;
            }
        }
        else
        {
            for (int i = 0; i < lvlInfos.score; i++)
            {
                scoreImages[i].color = Color.white;
            }
        }
        
        currentScore = lvlInfos.score;
        if (excedentaryScoreCounter == 0) return;
        Debug.Log(excedentaryScoreCounter);
        excedentaryScore.enabled = true;
        excedentaryScore.text = "+" + excedentaryScoreCounter;
    }
}
