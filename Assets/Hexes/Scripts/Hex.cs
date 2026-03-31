using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HexState
{
    None,
    Idle,
    Hovered
}

public abstract class Hex : MonoBehaviour
{
    [SerializeField] protected Vector2Int relativeCoords;
    public List<Grabbable> grabbablesOnThisHex = new List<Grabbable>();
    public Vector3 boundsCenter;

    [SerializeField] protected Grabber[] grabbers;
    public HexState state = HexState.Idle;
    [SerializeField] private Material idleMaterial;
    [SerializeField] private Material hoveredMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        boundsCenter = meshRenderer.bounds.center;
    }
    
    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        bool isHovered = false;
        
        for (int i = 0; i < grabbers.Length; i++)
        {
            if (grabbers[i].currentHoveredHex == meshRenderer.gameObject)
            {
                state = HexState.Hovered;
                isHovered = true;
            }
        }

        if (!isHovered)
        {
            state = HexState.Idle;
        }
        
        switch (state)
        {
            case HexState.Idle:
                ChangeMaterial(idleMaterial);
                break;
            case HexState.Hovered:
                ChangeMaterial(hoveredMaterial);
                break;
            default:
                ChangeMaterial(idleMaterial);
                break;
        }
    }

    public virtual void Tick()
    {
        grabbablesOnThisHex.First().Tick();
    }

    public void ChangeMaterial(Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }

    public virtual void AddGrabbable(Grabbable grabbable)
    {
        grabbablesOnThisHex.Add(grabbable);
        grabbablesOnThisHex.TrimExcess();
        grabbable.actualHex = this;
    }

    public virtual void RemoveGrabbable(Grabbable grabbable)
    {
        grabbablesOnThisHex.Remove(grabbable);
        grabbablesOnThisHex.TrimExcess();
        grabbable.actualHex = null;
    }

    public virtual void DestroyGrabbables(Grabbable grabbable)
    {
        grabbablesOnThisHex.Remove(grabbable);
        grabbablesOnThisHex.TrimExcess();
        Destroy(grabbable.gameObject);
    }

    public List<Grabbable> AllNextGrabbables(Grabbable grabbable)
    {
        List<Grabbable> allNextGrabbables = grabbablesOnThisHex.ToList(); 
        allNextGrabbables.RemoveRange(0, grabbablesOnThisHex.IndexOf(grabbable) + 1);
        return allNextGrabbables;
    }
}
