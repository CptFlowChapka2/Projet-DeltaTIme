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
    public bool debugMode = true;
    [SerializeField] private Rule[] rules;
    private IngredientsDictionary ingredientsDictionary;
    private Rule actualRuleToFollow;
    private List<Ingredient> workedIngredients=new List<Ingredient>();
    private bool isWorking = false;

    [SerializeField] private GameObject prefabIngredient;

    private int tickCounter = 0;

    protected override void Start()
    {
        base.Start();
        ingredientsDictionary = FindAnyObjectByType<IngredientsDictionary>();
        actualHex.AddGrabbable(this);
        
    }

    private void Update()
    {
        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Tick();
            }
        }
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
            isWorking = false;
            if (workedIngredients.Count > 0)
            {
                foreach (Grabbable grabbable in workedIngredients)
                {
                    actualHex.DestroyGrabbables(grabbable);
                } 
            }
            
            if (actualRuleToFollow.outputs.Contains(IngredientType.None)) return;
                
            foreach (IngredientType output in actualRuleToFollow.outputs)
            {
                Ingredient outputIngredient = Instantiate(prefabIngredient, transform.position, transform.rotation).GetComponent<Ingredient>();
                outputIngredient.Initialize(output, ingredientsDictionary,actualHex);
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
