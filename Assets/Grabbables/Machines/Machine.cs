using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct MachineRule : IEquatable<MachineRule>
{
    public IngredientType[] inputs;
    public IngredientType[] outputs;
    public int numberOfTicksToPerform;
    
    //pour pouvoir vérifier si 2 MachineRule sont égal//
    public bool Equals(MachineRule other)
    {
        return Equals(inputs, other.inputs) && Equals(outputs, other.outputs) && numberOfTicksToPerform == other.numberOfTicksToPerform;
    }

    public override bool Equals(object obj)
    {
        return obj is MachineRule other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(inputs, outputs, numberOfTicksToPerform);
    }
}

public class Machine : Grabbable
{
    public bool debugMode = true;
    public string machineName;
    [SerializeField] public MachineRule[] rules;
    private IngredientsDictionary ingredientsDictionary;
    private MachineRule _actualMachineRuleToFollow;
    private MachineRule _lastMachineRuleToFollow;
    private List<Ingredient> workedIngredients=new List<Ingredient>();
    private bool isWorking = false;
    public int possibleSpeedBoostByEnergizer = 0;

    [SerializeField] private GameObject prefabIngredient;

    [SerializeField] public Material popupMaterial;
    [SerializeField] public GameObject loadingBarFrame;
    [SerializeField] public LoadingBar loadingBar;

    [HideInInspector]public int tickCounter = 0;
    public int timeToReactivate = 3;
    private LvlInfos _lvlInfos;
    
    

    protected override void Start()
    {
        base.Start();
        ingredientsDictionary = FindAnyObjectByType<IngredientsDictionary>();
        _lvlInfos = FindAnyObjectByType<LvlInfos>();
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
            _lastMachineRuleToFollow = new MachineRule();
            _actualMachineRuleToFollow = new MachineRule();
            isActive = true;
            ActualVariant = beforeGrabVariant;
            tickCounter = 0;
            return;
        }

        AssignRule();
        if (_actualMachineRuleToFollow.Equals(new MachineRule())) return;
        ApplyRule();
    }

    private void ApplyRule()
    {
        loadingBar.ExtendLoadingBar();
        tickCounter++;
        if (tickCounter >= _actualMachineRuleToFollow.numberOfTicksToPerform - possibleSpeedBoostByEnergizer)
        {
            loadingBar.ChangeColor(Color.yellow);
            soundManager.numberOfMachinesWorking--;
            soundManager.finishRecipe.Play();
            tickCounter = 0;
            // todo pour les variants
            if (workedIngredients.Count > 0)
            {
                foreach (Grabbable grabbable in workedIngredients)
                {
                    actualHex.DestroyGrabbables(grabbable);
                } 
            }
            
            if (_actualMachineRuleToFollow.outputs.Contains(IngredientType.None)) return;
            if (_actualMachineRuleToFollow.outputs.Contains(IngredientType.Score))
            {
                foreach (IngredientType score in _actualMachineRuleToFollow.outputs)
                { 
                    _lvlInfos.score++;
                }
                return;
            }
                
            foreach (IngredientType output in _actualMachineRuleToFollow.outputs)
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
        
        _actualMachineRuleToFollow = new MachineRule();
        
        foreach (MachineRule rule in rules)
        {
            //prend en priorité les rélge spawner si présente
            if (rule.inputs.Contains(IngredientType.None) && ingredientsTypes.Count == 0)
            {
                _actualMachineRuleToFollow = rule;
                soundManager.numberOfMachinesWorking++;
                break;
            }

            
            var intersection =ingredientsTypes.Where(x => rule.inputs.Contains(x)).ToList();
            //Ca doit être exactement la même, mais pas forcément dans le même ordre
            var intersectionIngredientsTypes = intersection.ToList();
            
            if (intersectionIngredientsTypes.Count() == rule.inputs.Length//verifie que l'on as suffisament pour que la régle fonctionne
                && intersectionIngredientsTypes.Count() == ingredientsTypes.Count //verifie que on as bien que les ingredient qu'on as
                && ingredientsTypes.Count == nextGrabbables.Count)
            {
                _actualMachineRuleToFollow = rule;
                soundManager.numberOfMachinesWorking++;
                break;
            }
        }

        if (_actualMachineRuleToFollow.Equals(new MachineRule()))
        {
            //loadingBar update
            loadingBar.Initialize(0);
            loadingBarFrame.SetActive(false);
            
            _lastMachineRuleToFollow = _actualMachineRuleToFollow;
            return;
        }
        
        if (!_actualMachineRuleToFollow.Equals(_lastMachineRuleToFollow))
        {
            //loadingBar update
            loadingBarFrame.SetActive(true);
            loadingBar.Initialize(_actualMachineRuleToFollow.numberOfTicksToPerform);
            
            tickCounter = 0;
            workedIngredients.Clear();
            if (!(_actualMachineRuleToFollow.inputs.Contains(IngredientType.None)))
            {
                nextGrabbables.ForEach(x => workedIngredients.Add((Ingredient)x));  
            }
        }
        
        _lastMachineRuleToFollow = _actualMachineRuleToFollow;
    }
}
