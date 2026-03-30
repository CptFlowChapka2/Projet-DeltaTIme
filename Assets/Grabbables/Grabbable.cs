using System;
using UnityEngine;

[Serializable]
public struct GrabbableVariant
{
    public Mesh mesh;
    public Material material;
}

public abstract class Grabbable : MonoBehaviour
{
    public GrabbableVariant[] variants;
    private int actualVariant;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    
    public Hex actualHex;
    public bool isGrabbed = false;
    public bool isActive = true;

    public int ActualVariant
    {
        get => actualVariant;
        set
        {
            int clampValue =  Mathf.Clamp(value, 0, variants.Length - 1);
            actualVariant = clampValue;
            meshRenderer.material = variants[actualVariant].material;
            meshFilter.mesh = variants[actualVariant].mesh;
        } 
    }
    
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        if(variants.Length==0)return;
        ActualVariant = 0;
    }

    protected virtual void Start()
    {
        transform.position = actualHex.boundsCenter + new Vector3(0, 0.5f, 0);
    }

    public virtual void Tick()
    {
        
    }
}
