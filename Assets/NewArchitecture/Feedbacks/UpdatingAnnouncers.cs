using System;
using UnityEngine;

public class UpdatingAnnouncers : FeedbackCaller
{
    [SerializeField] private int playerID;
    
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
    [SerializeField] private FeedbacksManager feedbacksManager;

    private void Start()
    {
        ReinitializeMaterials();
    }

    public override void Call()
    {
        int currentBeat = feedbacksManager.gameManager.GetBeat();
    }

    public void ListenForOnStartMeasure()
    {
        ReinitializeMaterials();
    }

    private void ReinitializeMaterials()
    {
        baseMaterial = beat1Announcer.material;
        baseMaterial = beat2Announcer.material;
        baseMaterial = beat3Announcer.material;
        baseMaterial = beat4Announcer.material;
    }
}
