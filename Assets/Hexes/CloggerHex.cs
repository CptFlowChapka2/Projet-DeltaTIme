using System.Linq;
using UnityEngine;

public class CloggerHex : Hex
{
    [SerializeField] private GameObject cloggerObject;
    [SerializeField] private int numberOfTicksToClog = 2;
    [SerializeField] private int numberOfObjectToClog = 2;
    [SerializeField] private IngredientType clogType;
    [SerializeField] private ParticleSystem particles;
    private IngredientsDictionary ingredientsDictionary;
    private int numberOfTicks;
    public bool permaClogger = false;

    protected override void Start()
    {
        base.Start();
        ingredientsDictionary = FindAnyObjectByType<IngredientsDictionary>();
        particles.Stop();
    }
    
    public override void Tick()
    {
        //Base
        if(grabbablesOnThisHex.Count==0)return;
        if (hexRules.Length != 0)
        {
            VerifyHexRule();
            UpdateFeedbackTimer();
        }
        else
        {
            tickCounter = 0;
            lastTickRule = new HexRule();
        }
        
        //end base
        
        if (VerifyAndClog()) return;
        grabbablesOnThisHex?.First()?.Tick();
    }

    private bool VerifyAndClog()
    {
        if (grabbablesOnThisHex.Last() is Ingredient &&(!permaClogger&& ((Ingredient)grabbablesOnThisHex.Last()).type == clogType)) return false;
        if (grabbablesOnThisHex.First() is not null)
        {
            FeedbackClogging();
            //contournement , le clogger fait ticker les machine inactive 
            if(grabbablesOnThisHex.First() is Machine&&
               !((Machine)grabbablesOnThisHex.First()).isActive)((Machine)
                grabbablesOnThisHex.First()).Tick();
            numberOfTicks++;
            if (numberOfTicks >= numberOfTicksToClog)
            {
                StopFeedbackClogging();
                numberOfTicks = 0;
                for (int i = 0; i < numberOfObjectToClog; i++)
                {
                    Ingredient clogIngredient = Instantiate(cloggerObject,
                        transform.position + new Vector3(0f, 0.5f, 0f),
                        Quaternion.identity).GetComponent<Ingredient>();
                    clogIngredient.Initialize(clogType, ingredientsDictionary, this);
                }
                return true;
            }
            
            
        }

        return false;
    }

    private void FeedbackClogging()
    {
        if (clogType == IngredientType.Araignée)
        {
            soundManager.forestTilesActivated++;
        }
        else if (clogType == IngredientType.LiquideMagique)
        {
            soundManager.lakeTilesActivated++;
        }
        
        particles.Play();
    }
    
    private void StopFeedbackClogging()
    {
        if (clogType == IngredientType.Araignée && soundManager.forestTilesActivated > 0)
        {
            soundManager.forestTilesActivated--;
        }
        else if (clogType == IngredientType.LiquideMagique && soundManager.lakeTilesActivated > 0)
        {
            soundManager.lakeTilesActivated--;
        }
        
        particles.Stop();
    }
    
    public override void RemoveGrabbable(Grabbable grabbable)
    {
        if(grabbablesOnThisHex.First().Equals(grabbable))numberOfTicks = 0;
        base.RemoveGrabbable(grabbable);
        
    }
}
