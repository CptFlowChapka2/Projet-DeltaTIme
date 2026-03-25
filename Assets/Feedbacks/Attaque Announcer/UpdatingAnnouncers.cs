using System;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingAnnouncers : FeedbackCaller
{
     public int playerID;
    
    [Header("Announcers")]
    [SerializeField] private MeshRenderer beat1Announcer;
    [SerializeField] private MeshRenderer beat2Announcer;
    [SerializeField] private MeshRenderer beat3Announcer;
    [SerializeField] private MeshRenderer beat4Announcer;
    
    [Header("Materials")]
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material rightMaterial;
    [SerializeField] private Material leftMaterial;
    [SerializeField] private Material upMaterial;
    [SerializeField] private Material downMaterial;
    [SerializeField] private Material crossMaterial;
    
    [Header("FM")]
     private FeedbacksManager feedbacksManager;

    private void Start()
    {
        feedbacksManager = (FeedbacksManager)manager;
        ReinitializeMaterials();
    }

    public void ListenForOnMeasure()
    {
        ReinitializeMaterials();
    }

    public void ListenForOnEndCoyoteBeat()
    {
        Vector2Int[] inputsThisMeasureCache = feedbacksManager.gameManager.GetInputsThisMeasureCache(playerID);
        Vector2Int nullInput = new Vector2Int(-1, -1);
        
        if (inputsThisMeasureCache[3] != nullInput)
        {
            beat4Announcer.material = ConvertInputIntoMaterial(inputsThisMeasureCache[3]);
        }
        if (inputsThisMeasureCache[2] != nullInput)
        {
            beat3Announcer.material = ConvertInputIntoMaterial(inputsThisMeasureCache[2]);
        }
        if (inputsThisMeasureCache[1] != nullInput)
        {
            beat2Announcer.material = ConvertInputIntoMaterial(inputsThisMeasureCache[1]);
        }
        if (inputsThisMeasureCache[0] != nullInput)
        {
            beat1Announcer.material = ConvertInputIntoMaterial(inputsThisMeasureCache[0]);
        }
    }

    private Material ConvertInputIntoMaterial(Vector2Int input)
    {
        if (input == Vector2Int.zero)
        {
            return crossMaterial;
        }
        else if (input == Vector2Int.left)
        {
            return leftMaterial;
        }
        else if (input == Vector2Int.right)
        {
            return rightMaterial;
        }
        else if (input == Vector2Int.up)
        {
            return upMaterial;
        }
        else if (input == Vector2Int.down)
        {
            return downMaterial;
        }
        else
        {
            return baseMaterial;
        }
    }

    private void ReinitializeMaterials()
    {
        beat1Announcer.material = baseMaterial;
        beat2Announcer.material = baseMaterial;
        beat3Announcer.material = baseMaterial;
        beat4Announcer.material = baseMaterial;
    }
}
