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
    public Vector2Int relativeCoords;
    public List<Grabbable> grabbablesOnThisHex = new List<Grabbable>();
    //public Vector3 boundsCenter;

    public Grabber[] grabbers;
    public HexState state = HexState.Idle;
    public int maxNbrOfGrabbable = 3;
    [SerializeField] private Material idleMaterial;
    [SerializeField] private Material hoveredMaterial;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material popupBaseMaterial;
    [HideInInspector]public GridManager gridManager;
    [HideInInspector]public LvlInfos lvlInfos;

    protected virtual void Start()
    {
        gridManager = FindAnyObjectByType<GridManager>();
        lvlInfos = FindAnyObjectByType<LvlInfos>();
        lvlInfos.Tick.AddListener(Tick);
        grabbers = gridManager.grabbers;

        InitializeRelativeCoords();
    }

    private void InitializeRelativeCoords()
    {
        int relativeX = 0;
        int relativeY = 0;

        float stepsX = transform.position.x;
        float stepsY = transform.position.z;

        for (int i = 0; i < 25; i++)
        {
            if ((stepsX > -0.1f && stepsX < 0.1f) || (stepsX >= 0.866024f - 0.1f && stepsX < 0.866024f + 0.1f)) //approximation car Mathf.Sqrt est pas précise
            {
                relativeX = i;
            }
            
            if (stepsY == 0)
            {
                relativeY = i;
            }
            
            stepsX -= 0.866024f * 2f;
            stepsY -= 1.5f;
        }
        
        relativeCoords.x = relativeX;
        relativeCoords.y = relativeY;
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
                if (grabbablesOnThisHex.Count == 0 || grabbablesOnThisHex.First() is not Machine)
                {
                    grabbers[i].player.popupRenderer.material = popupBaseMaterial;
                } 
                else
                {
                    Machine machine = grabbablesOnThisHex.First() as Machine;
                    grabbers[i].player.popupRenderer.material = machine.popupMaterial;
                }
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
        if(grabbablesOnThisHex.Count==0)return;
        grabbablesOnThisHex?.First()?.Tick();
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
        ReOrganiseGrabbable();
    }

    public virtual void RemoveGrabbable(Grabbable grabbable)
    {
        grabbablesOnThisHex.Remove(grabbable);
        grabbablesOnThisHex.TrimExcess();
        grabbable.actualHex = null;
        ReOrganiseGrabbable();
    }

    public virtual void DestroyGrabbables(Grabbable grabbable)
    {
        grabbablesOnThisHex.Remove(grabbable);
        grabbablesOnThisHex.TrimExcess();
        ReOrganiseGrabbable();
        if(grabbable ==null)return;
        Destroy(grabbable.gameObject);
        
    }

    public List<Grabbable> AllNextGrabbables(Grabbable grabbable)
    {
        List<Grabbable> allNextGrabbables = grabbablesOnThisHex.ToList(); 
        allNextGrabbables.RemoveRange(0, grabbablesOnThisHex.IndexOf(grabbable) + 1);
        return allNextGrabbables;
    }

    public void ReOrganiseGrabbable()
    {
        grabbablesOnThisHex.TrimExcess();
        Vector3 a = new Vector3(0, 0.15f, 0);
        for (int i = 0; i < grabbablesOnThisHex.Count; i++)
        {
            grabbablesOnThisHex[i].transform.position = transform.position + a;
            a += a;
        }
    }
}
