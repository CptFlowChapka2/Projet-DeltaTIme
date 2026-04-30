using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private IngredientsDictionary ingredientsDictionary;
    [SerializeField] private Grabber grabber;
    private Hex hex;
    private List<Grabbable> grabbables;
    
    [Header("IngredientsStack")]
    public Image[] stack;
    [Header("MachineRules")]
    public TMP_Text machineName;
    public GameObject machineUIBox;
    public Image[] rule1Inputs;
    public Image[] rule1Outputs;
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

    private void Update()
    {
        Debug.Log("ui");
        hex = grabber.currentHoveredHex;
        if (!hex) return;
        
        hexName.text = hex.hexName;
        //todo éléments de HexRules à afficher

        grabbables = new List<Grabbable>(hex.grabbablesOnThisHex);
        machineUIBox.SetActive(false);
        
        if (grabbables.Count > 0)
        {
            if (grabbables.First() is Machine)
            {
                machineUIBox.SetActive(true);
                Machine machine = grabbables.First() as Machine;
                machineName.text = machine.machineName;
                //todo infos de machines
                grabbables.Remove(grabbables.First());
            }
            
            int numberOfEmptySlotsInIngredientsUIBox = stack.Length - grabbables.Count;
            for (int i = 0; i < stack.Length; i++)
            {
                if (i >= stack.Length - numberOfEmptySlotsInIngredientsUIBox)
                {
                    stack[i].enabled = false;
                }
                else
                {
                    Image image = stack[i];
                    image.enabled = true;
                    Ingredient ingredientToShow = grabbables[i] as Ingredient;
                    image.sprite = ingredientToShow.variants[ingredientToShow.ActualVariant].sprite;
                }
            }
            
        }
    }
}
