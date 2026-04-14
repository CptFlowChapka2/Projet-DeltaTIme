using System.Linq;
using UnityEngine;

public class CloggerHex : Hex
{
    [SerializeField] private GameObject cloggerObject;
    [SerializeField] private int numberOfTicksToClog = 2;
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
            ((Ingredient)grabbablesOnThisHex.Last()).type == IngredientType.Clogger) return;
        if (grabbablesOnThisHex.First() is Machine)
        {
            numberOfTicks++;
            if (numberOfTicks >= numberOfTicksToClog)
            {
                numberOfTicks = 0;
                Ingredient clogIngredient = Instantiate(cloggerObject,
                    transform.position + new Vector3(0f, 0.5f, 0f),
                    Quaternion.identity).GetComponent<Ingredient>();
                clogIngredient.Initialize(IngredientType.Clogger, ingredientsDictionary, this);
            }
        }
        grabbablesOnThisHex?.First()?.Tick();
    }
}
