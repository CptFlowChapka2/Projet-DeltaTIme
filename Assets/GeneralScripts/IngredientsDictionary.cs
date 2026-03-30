using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum IngredientType
{
    None,
    A,
    B
}

public class IngredientsDictionary : MonoBehaviour
{
    [SerializedDictionary("Type", "IngredientRenderPrefab")] 
    public SerializedDictionary<IngredientType, GrabbableVariant[]> ingredients =
        new SerializedDictionary<IngredientType, GrabbableVariant[]>();
}
