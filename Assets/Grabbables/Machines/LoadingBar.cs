using System;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Transform finalTransform;
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
    }

    public void ChangeColor(Color color)
    {
        meshRenderer.material.color = color;
    }
}
