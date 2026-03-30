using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct Rule
{
    public IngredientType[] inputs;
    public IngredientType[] outputs;
    public int numberOfTicksToPerform;
}

public class Machine : Grabbable
{
    [SerializeField] private Rule[] rules;
    private IngredientsDictionary ingredientsDictionary;
    private Rule actualRuleToFollow;
    private List<Ingredient> workedIngredients;
    private bool isWorking = false;

    [SerializeField] private GameObject prefabIngredient;

    private int tickCounter = 0;

    private void Start()
    {
        ingredientsDictionary = FindObjectOfType<IngredientsDictionary>();
    }

    public override void Tick()
    {
        if (!isWorking)
        {
            AssignRule();
        }

        if (isWorking)
        {
            ApplyRule();
        }
    }

    private void ApplyRule()
    {
        tickCounter++;
        if (tickCounter >= actualRuleToFollow.numberOfTicksToPerform)
        {
            tickCounter = 0;
            // todo pour les variants
            foreach (Grabbable grabbable in workedIngredients)
            {
                actualHex.DestroyGrabbables(grabbable);
            }

            if (actualRuleToFollow.outputs.Contains(IngredientType.None)) return;
                
            foreach (IngredientType output in actualRuleToFollow.outputs)
            {
                Ingredient outputIngredient = Instantiate(prefabIngredient, transform.position, transform.rotation).GetComponent<Ingredient>();
                outputIngredient.Initialize(output, ingredientsDictionary);
                actualHex.AddGrabbable(outputIngredient);
            }
        }
    }

    private void AssignRule()
    {
        List<Grabbable> nextGrabbables = actualHex.AllNextGrabbables(this);
        List<IngredientType> ingredientsTypes = new List<IngredientType>();

        foreach (Grabbable grabbable in nextGrabbables)
        {
            if (grabbable is Ingredient)
            {
                ingredientsTypes.Add(((Ingredient)grabbable).type);
            }
        }
        
        foreach (Rule rule in rules)
        {
            if (rule.inputs.Contains(IngredientType.None) && ingredientsTypes.Count == 0)
            {
                actualRuleToFollow = rule;
                isWorking = true;
                break;
            }

            var intersection = ingredientsTypes.Intersect(rule.inputs.ToList());
            //Ca doit être exactement la même, mais pas forcément dans le même ordre
            if (intersection.Count() == rule.inputs.Length && intersection.Count() == ingredientsTypes.Count &&
                ingredientsTypes.Count == nextGrabbables.Count)
            {
                actualRuleToFollow = rule;
                nextGrabbables.ForEach(x => workedIngredients.Add((Ingredient)x));
                isWorking = true;
                break;
            }
        }
    }
}
