using System;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Transform finalTransform;
    [SerializeField] private Transform startingTransform;
    [SerializeField] private MeshRenderer meshRenderer;
    public float localTickCounter = 0;
    public float numberOfTicksOnRuleToFollow = 0;

    public void Initialize(float ruleTicks)
    {
        ChangeColor(Color.blue);
        localTickCounter = 0;
        numberOfTicksOnRuleToFollow = ruleTicks;
        if (numberOfTicksOnRuleToFollow == 0) return;
        transform.localScale = new Vector3(finalTransform.localScale.x / numberOfTicksOnRuleToFollow, 1f, 0.8f);
        transform.localPosition = new Vector3(startingTransform.localPosition.x, 0.01f, 0f);
    }
    
    public void ExtendLoadingBar()
    {
        localTickCounter++;
        if (localTickCounter > numberOfTicksOnRuleToFollow)
        {
            ChangeColor(Color.blue);
            localTickCounter = 1;
        }
        transform.localScale = new Vector3((finalTransform.localScale.x / numberOfTicksOnRuleToFollow) * localTickCounter, 1f, 0.8f);
        //ligne de l'enfer (set la position relativement à deux points pivots)
        transform.localPosition = new Vector3(startingTransform.localPosition.x + 
                                              (localTickCounter * (finalTransform.localPosition.x - startingTransform.localPosition.x) / numberOfTicksOnRuleToFollow), 0.01f, 0f);
    }

    public void ChangeColor(Color color)
    {
        meshRenderer.material.color = color;
    }
}
