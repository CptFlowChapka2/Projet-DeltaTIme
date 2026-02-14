using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IndividualCrowdScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> lvlsSprites;
    private SpriteRenderer thisSpriteRenderer;
    public CrowdTags[] thisTags;

    private void Start()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TryChangeLvl(int i)
    {
        
        if(i > lvlsSprites.Count - 1) return;
        thisSpriteRenderer.sprite = lvlsSprites[i];
    }
}

public enum CrowdTags
{
    Left,
    Right
}
