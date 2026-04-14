using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct Rule : IEquatable<Rule>
{
    public IngredientType[] inputs;
    public IngredientType[] outputs;
    public int numberOfTicksToPerform;

    //pour pouvoir vérifier si 2 Rule sont égal//
    public bool Equals(Rule other)
    {
        return Equals(inputs, other.inputs) && Equals(outputs, other.outputs) && numberOfTicksToPerform == other.numberOfTicksToPerform;
    }

    public override bool Equals(object obj)
    {
        return obj is Rule other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(inputs, outputs, numberOfTicksToPerform);
    }
}

public class Machine : Grabbable
{
    public bool debugMode = true;
    [SerializeField] private Rule[] rules;
    private IngredientsDictionary ingredientsDictionary;
    private Rule actualRuleToFollow;
    private Rule lastRuleToFollow;
    private List<Ingredient> workedIngredients=new List<Ingredient>();
    private bool isWorking = false;
    public int possibleSpeedBoostByEnergizer = 0;

    [SerializeField] private GameObject prefabIngredient;

    [SerializeField] public Material popupMaterial;

    private int tickCounter = 0;
    public int timeToReactivate = 3;
    private Timer _timer;
    
    

    protected override void Start()
    {
        base.Start();
        ingredientsDictionary = FindAnyObjectByType<IngredientsDictionary>();
        _timer = actualHex.timer;
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
        if (!isActive)
        {
            tickCounter++;
            if (tickCounter < timeToReactivate) return;
            isActive = true;
            ActualVariant = beforeGrabVariant;
            tickCounter = 0;
            return;
        }

        AssignRule();
        if (actualRuleToFollow.Equals(new Rule())) return;
        ApplyRule();
    }

    private void ApplyRule()
    {
        tickCounter++;
        if (tickCounter >= actualRuleToFollow.numberOfTicksToPerform - possibleSpeedBoostByEnergizer)
        {
            tickCounter = 0;
            // todo pour les variants
            if (workedIngredients.Count > 0)
            {
                foreach (Grabbable grabbable in workedIngredients)
                {
                    actualHex.DestroyGrabbables(grabbable);
                } 
            }
            
            if (actualRuleToFollow.outputs.Contains(IngredientType.None)) return;
            if (actualRuleToFollow.outputs.Contains(IngredientType.Score))
            {
                foreach (IngredientType score in actualRuleToFollow.outputs)
                { 
                    _timer.score++;
                }
                return;
            }
                
            foreach (IngredientType output in actualRuleToFollow.outputs)
            {
                Ingredient outputIngredient = Instantiate(prefabIngredient, transform.position, transform.rotation).GetComponent<Ingredient>();
                outputIngredient.Initialize(output, ingredientsDictionary, actualHex);
                //l'ingredient qui vient de spawn applique son Start de grabble donc s'ajoute lui même à la hex//
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
        
        actualRuleToFollow = new Rule();
        
        foreach (Rule rule in rules)
        {
            if (rule.inputs.Contains(IngredientType.None) && ingredientsTypes.Count == 0)
            {
                actualRuleToFollow = rule;
                break;
            }

            var intersection = ingredientsTypes.Intersect(rule.inputs.ToList());
            //Ca doit être exactement la même, mais pas forcément dans le même ordre
            var ingredientTypes = intersection.ToList();
            if (ingredientTypes.Count() == rule.inputs.Length && ingredientTypes.Count() == ingredientsTypes.Count &&
                ingredientsTypes.Count == nextGrabbables.Count)
            {
                actualRuleToFollow = rule;
                break;
            }
        }

        if (actualRuleToFollow.Equals(new Rule()))
        {
            lastRuleToFollow = actualRuleToFollow;
            return;
        }
        
        if (!actualRuleToFollow.Equals(lastRuleToFollow))
        {
            tickCounter = 0;
            workedIngredients.Clear();
            if (!(actualRuleToFollow.inputs.Contains(IngredientType.None)))
            {
                nextGrabbables.ForEach(x => workedIngredients.Add((Ingredient)x));  
            }
        }
        
        lastRuleToFollow = actualRuleToFollow;
    }
}
