using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum IngredientType
{
    None = 0,
    Score = 1,
    A = 2,
    B = 3,
    C = 4,
    D = 5,
    E = 6,
    F = 7,
    Clogger = 100
}

public class IngredientsDictionary : MonoBehaviour
{
    [SerializedDictionary("Type", "IngredientRenderPrefab")] 
    public SerializedDictionary<IngredientType, GrabbableVariant[]> ingredients =
        new SerializedDictionary<IngredientType, GrabbableVariant[]>();
    
}
