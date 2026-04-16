using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum IngredientType
{
    None = 0,
    Score = 1,
    Crabiroche = 2,
    Magie_en_bouteille = 3,
    Crabébou = 4,
    Crabisilice = 5,
    Crabisilice_Enchanté = 6,
    Poudre_de_Crabisilice_enchanté = 7,
    Araignée_Mage=8,
    Soi_Magique=9,
    Nué=10,
    LiquideMagique = 100,
    Araigné = 101
}

public class IngredientsDictionary : MonoBehaviour
{
    [SerializedDictionary("Type", "IngredientRenderPrefab")] 
    public SerializedDictionary<IngredientType, GrabbableVariant[]> ingredients =
        new SerializedDictionary<IngredientType, GrabbableVariant[]>();
    
}
