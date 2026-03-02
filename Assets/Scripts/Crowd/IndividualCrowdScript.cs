using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IndividualCrowdScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> lvlsSprites;
    private SpriteRenderer thisSpriteRenderer;
    public CrowdTags[] thisTags;
    private int currentSpriteLevel = 0;

    private void Start()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisSpriteRenderer.sprite = lvlsSprites[currentSpriteLevel];
    }

    public void TryChangeLvl(int i)
    {
        currentSpriteLevel += i;
        
        if (currentSpriteLevel >= lvlsSprites.Count)
        {
            currentSpriteLevel = lvlsSprites.Count - 1;
        }
        else if (currentSpriteLevel < 0)
        {
            currentSpriteLevel = 0;
        }
        
        thisSpriteRenderer.sprite = lvlsSprites[currentSpriteLevel];
    }
}

public enum CrowdTags
{
    Left,
    Right
}
