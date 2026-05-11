using System;
using UnityEngine;

public class Ingredient : Grabbable
{
    public IngredientType type;
    [SerializeField] private SpriteRenderer ingredientStickerRenderer;
    private IngredientsDictionary dictionary; 

    public void Initialize(IngredientType ingredientType, IngredientsDictionary ingredientsDictionary, Hex hex)
    {
         type = ingredientType;
         dictionary = ingredientsDictionary;
         variants = dictionary.AskForVariants(ingredientType);
         ActualVariant = 0;
         ingredientStickerRenderer.sprite = idSprite;
         actualHex = hex;
         transform.position = actualHex.gameObject.transform.position + new Vector3(0, 0.5f, 0);
    }

    protected override void Start()
    {
        base.Start();
    }
}
