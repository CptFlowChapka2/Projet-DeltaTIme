using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum IngredientType
{
    None,
    Score,
    A,
    B,
    C,
    D,
    E,
    F
}

public class IngredientsDictionary : MonoBehaviour
{
    [SerializedDictionary("Type", "IngredientRenderPrefab")] 
    public SerializedDictionary<IngredientType, GrabbableVariant[]> ingredients =
        new SerializedDictionary<IngredientType, GrabbableVariant[]>();
    
}
