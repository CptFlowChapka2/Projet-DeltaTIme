using System;
using UnityEngine;

public class Ingredient : Grabbable
{
    public IngredientType type;
    private IngredientsDictionary dictionary; 

    public void Initialize(IngredientType ingredientType, IngredientsDictionary ingredientsDictionary)
    {
         type = ingredientType;
         dictionary = ingredientsDictionary;
         variants = dictionary.ingredients[type];
         ActualVariant = 0;
    }
}
