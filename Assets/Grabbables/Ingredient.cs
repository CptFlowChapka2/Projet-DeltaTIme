using System;
using UnityEngine;

public enum IngredientType
{
    None,
    A,
    B
}

public class Ingredient : Grabbable
{
    public IngredientType type;

    public void Initialize(IngredientType ingredientType)
    {
        type = ingredientType;
    }
}
