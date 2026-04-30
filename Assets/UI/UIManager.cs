using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private IngredientsDictionary ingredientsDictionary;
    private Grabber grabber;
    private Hex hex;
    private List<Grabbable> grabbables;
    
    [Header("IngredientsStack")]
    public Image[] stack;
    [Header("MachineRules")]
    public TMP_Text machineName;
    public GameObject machineUIBox;
    public Image[] rule1Inputs;
    public Image[] rule1Outputs;
    public Image rule2Arrow;
    public Image[] rule2Inputs;
    public Image[] rule2Outputs;
    [Header("HexRules")]
    public TMP_Text hexName;
    public Image element1;
    public Image element2;
    public Image element3;
    public Image element4;
    public Image element5;
    public Image element6;
    public Image element7;

    private void Start()
    {
        if (this.name == "UILeft")
        {
            grabber = FindAnyObjectByType<GridManager>().grabbers[0];
        }
        else
        {
            grabber = FindAnyObjectByType<GridManager>().grabbers[1];
        }
    }

    private void Update()
    {
        machineUIBox.SetActive(false);
        hex = grabber.currentHoveredHex;
        if (!hex) return;
        
        DisplayHexUI();

        grabbables = new List<Grabbable>(hex.grabbablesOnThisHex);
        
        if (grabbables.Count > 0)
        {
            if (grabbables.First() is Machine)
            {
                DisplayMachineUI();
            }
        }
        
        DisplayIngredientsUI();
    }

    private void DisplayHexUI()
    {
        hexName.text = hex.hexName;
        //todo éléments de HexRules à afficher
    }

    private void DisplayIngredientsUI()
    {
        for (int i = 0; i < stack.Length; i++)
        {
            if (i >= grabbables.Count)
            {
                stack[i].enabled = false;
            }
            else
            {
                Image image = stack[i];
                image.enabled = true;
                image.sprite = grabbables[i].variants[grabbables[i].ActualVariant].sprite;
            }
        }
    }

    private void DisplayMachineUI()
    {
        machineUIBox.SetActive(true);
        Machine machine = grabbables.First() as Machine;
        machineName.text = machine.machineName;
                
        DisplayMachineRule(machine.rules[0], rule1Inputs, rule1Outputs);
        if (machine.rules.Length == 2)
        {
            rule2Arrow.enabled = true;
            DisplayMachineRule(machine.rules[1], rule2Inputs, rule2Outputs);
        }
        else
        {
            for (int i = 0; i < rule1Inputs.Length; i++)
            {
                rule2Arrow.enabled = false;
                rule2Inputs[i].enabled = false;
                rule2Outputs[i].enabled = false;
            }
        }
                
        grabbables.Remove(grabbables.First());
    }

    private void DisplayMachineRule(MachineRule machineRule, Image[] inputs, Image[] outputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            //inputs
            if (i >= machineRule.inputs.Length)
            {
                inputs[i].enabled = false;
            }
            else
            {
                Image image = inputs[i];
                IngredientType ingredientTypeToShow = machineRule.inputs[i];
                inputs[i].enabled = true;
                image.sprite = ingredientsDictionary.ingredients[ingredientTypeToShow][0].sprite;
            }
            
            //outputs
            if (i >= machineRule.outputs.Length)
            {
                outputs[i].enabled = false;
            }
            else
            {
                Image image = outputs[i];
                IngredientType ingredientTypeToShow = machineRule.outputs[i];
                outputs[i].enabled = true;
                image.sprite = ingredientsDictionary.ingredients[ingredientTypeToShow][0].sprite;
            }
        }
    }
}
