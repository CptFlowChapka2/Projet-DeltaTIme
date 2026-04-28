using System;
using UnityEngine;

[Serializable]
public struct GrabbableVariant
{
    public Mesh mesh;
    public Material material;
    public Sprite sprite;
}

public abstract class Grabbable : MonoBehaviour
{
    public GrabbableVariant[] variants;
    private int actualVariant;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    public Sprite idSprite;
    
    public Hex actualHex;
    public bool isGrabbed = false;
    public bool isActive = true;
    public int beforeGrabVariant;
    
    protected SoundManager soundManager;

    public int ActualVariant
    {
        get => actualVariant;
        set
        {
            int clampValue =  Mathf.Clamp(value, 0, variants.Length - 1);
            actualVariant = clampValue;
            meshRenderer.material = variants[actualVariant].material;
            meshFilter.mesh = variants[actualVariant].mesh;
            idSprite = variants[actualVariant].sprite;
        } 
    }
    
    private void Awake()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
        if(variants.Length==0)return;
        ActualVariant = 0;
    }

    protected virtual void Start()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, 2f, transform.position.z), -transform.up, out RaycastHit hit))
        {
            transform.position = hit.collider.gameObject.transform.position + new Vector3(0, 0.5f, 0);
            if(actualHex is null)  actualHex = hit.collider.gameObject.GetComponentInParent<Hex>();
            actualHex.AddGrabbable(this) ;
        }
    }

    public virtual void Tick()
    {
        
    }
}
