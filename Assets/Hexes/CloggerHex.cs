using System.Linq;
using UnityEngine;

public class CloggerHex : Hex
{
    [SerializeField] private GameObject cloggerObject;
    [SerializeField] private int numberOfTicksToClog = 2;
    [SerializeField] private int numberOfObjectToClog = 2;
    [SerializeField] private IngredientType clogType;
    private IngredientsDictionary ingredientsDictionary;
    private int numberOfTicks;

    protected override void Start()
    {
        base.Start();
        ingredientsDictionary = FindAnyObjectByType<IngredientsDictionary>();
    }
    
    public override void Tick()
    {
        //Base
        if(grabbablesOnThisHex.Count==0)return;
        if (hexRules.Length != 0)
        {
            VerifyHexRule();
        }
        else
        {
            tickCounter = 0;
            lastTickRule = new HexRule();
        }
        
        //end base
        if (grabbablesOnThisHex.Last() is Ingredient &&
            ((Ingredient)grabbablesOnThisHex.Last()).type == clogType) return;
        if (grabbablesOnThisHex.First() is not null)
        {
            numberOfTicks++;
            if (numberOfTicks >= numberOfTicksToClog)
            {
                // if(((Machine)grabbablesOnThisHex.First()).isActive)
                // ((Machine)grabbablesOnThisHex.First()).tickCounter = 0;
                numberOfTicks = 0;
                for (int i = 0; i < numberOfObjectToClog; i++)
                {
                    Ingredient clogIngredient = Instantiate(cloggerObject,
                        transform.position + new Vector3(0f, 0.5f, 0f),
                        Quaternion.identity).GetComponent<Ingredient>();
                    clogIngredient.Initialize(clogType, ingredientsDictionary, this);
                }
                return;
            }
            
            
        }
        grabbablesOnThisHex?.First()?.Tick();
    }

    public override void RemoveGrabbable(Grabbable grabbable)
    {
        if(grabbablesOnThisHex.First().Equals(grabbable))numberOfTicks = 0;
        base.RemoveGrabbable(grabbable);
        
    }
}
