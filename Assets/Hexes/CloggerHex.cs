using System.Linq;
using UnityEngine;

public class CloggerHex : Hex
{
    [SerializeField] private GameObject cloggerObject;
    [SerializeField] private int numberOfTicksToClog = 2;
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
        if (grabbablesOnThisHex.Count == 0) return;
        if (grabbablesOnThisHex.Last() is Ingredient &&
            ((Ingredient)grabbablesOnThisHex.Last()).type == clogType) return;
        if (grabbablesOnThisHex.First() is Machine)
        {
            numberOfTicks++;
            if (numberOfTicks >= numberOfTicksToClog)
            {
                // if(((Machine)grabbablesOnThisHex.First()).isActive)
                // ((Machine)grabbablesOnThisHex.First()).tickCounter = 0;
                numberOfTicks = 0;
                Ingredient clogIngredient = Instantiate(cloggerObject,
                    transform.position + new Vector3(0f, 0.5f, 0f),
                    Quaternion.identity).GetComponent<Ingredient>();
                clogIngredient.Initialize(clogType, ingredientsDictionary, this);
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
