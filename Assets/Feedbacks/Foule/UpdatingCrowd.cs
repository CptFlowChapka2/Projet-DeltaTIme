using System;
using System.Linq;
using UnityEngine;

public class UpdatingCrowd : Doer
{
    [SerializeField] private int numberOfConsecutivePerfectMeasuresToChangeLevel = 4;
    
    [SerializeField] private CrowdId crowdP1;
    [SerializeField] private CrowdId crowdP2;

    [SerializeField] private FeedbacksManager feedbacksManager;
    
    private SpriteRenderer spriteRenderer1;
    private SpriteRenderer spriteRenderer2;
    
    private int p1Level = 0;
    private int p2Level = 0;

    private int numberOfConsecutiveSuccessesP1;
    private int numberOfConsecutiveSuccessesP2;

    private bool isAlreadyHitP1 = false;
    private bool isAlreadyHitP2 = false;

    private void Start()
    {
        spriteRenderer1 = crowdP1.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer2 = crowdP2.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer1.sprite = crowdP1.frame1Sprites[p1Level];
        spriteRenderer2.sprite = crowdP2.frame1Sprites[p2Level];
    }

    private void Update()
    {
        GetAllUsefulParameters();
        VerifyIfHit();
    }

    public override void GetAllUsefulParameters()
    {
        isAlreadyHitP1 = feedbacksManager.gameManager.GetIsAlreadyHit(1);
        isAlreadyHitP2 = feedbacksManager.gameManager.GetIsAlreadyHit(2);
    }
    
    private void VerifyIfHit()
    {
        if (isAlreadyHitP1)
        {
            numberOfConsecutiveSuccessesP1 = 0;
        }
        else if (isAlreadyHitP2)
        {
            numberOfConsecutiveSuccessesP2 = 0;
        }
    }

    private void UpdateSpritesFrames()
    {
        if (crowdP1.frame1Sprites.Contains(spriteRenderer1.sprite))
        {
            spriteRenderer1.sprite = crowdP1.frame2Sprites[p1Level];
            spriteRenderer2.sprite = crowdP2.frame2Sprites[p2Level];
        }
        else
        {
            spriteRenderer1.sprite = crowdP1.frame1Sprites[p1Level];
            spriteRenderer2.sprite = crowdP2.frame1Sprites[p2Level];
        }
    }
    
    private int SetLevelDependingOnSuccesses(int successes)
    {
        if (successes < numberOfConsecutivePerfectMeasuresToChangeLevel)
        {
            return 0;
        }
        else if (successes >= numberOfConsecutivePerfectMeasuresToChangeLevel &&
                 successes < 2 * numberOfConsecutivePerfectMeasuresToChangeLevel)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public void ListenForOnBeat()
    {
        UpdateSpritesFrames();
    }

    public void ListenForOnMeasure()
    {
        numberOfConsecutiveSuccessesP1++;
        numberOfConsecutiveSuccessesP2++;
        
        p1Level = SetLevelDependingOnSuccesses(numberOfConsecutiveSuccessesP1);
        p2Level = SetLevelDependingOnSuccesses(numberOfConsecutiveSuccessesP2);
    }
}
