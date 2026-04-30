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
    Araignée_Mage = 8,
    Soie_Magique = 9,
    Nuée = 10,
    Poudre_de_Crabisilice = 11,
    Soie=12,
    LiquideMagique = 100,
    Araignée = 101
}

public class IngredientsDictionary : MonoBehaviour
{
    private LvlInfos _lvlInfos;

    private void Awake()
    {
        _lvlInfos = GetComponent<LvlInfos>();
    }

    [SerializedDictionary("Type", "IngredientRenderPrefab")] 
    public SerializedDictionary<IngredientType, GrabbableVariant[]> ingredients =
        new SerializedDictionary<IngredientType, GrabbableVariant[]>();

    public GrabbableVariant[] AskForVariants(IngredientType oi)
    {
        _lvlInfos.CheckForValidIngredient(oi);
        return ingredients[oi];
    }
}
