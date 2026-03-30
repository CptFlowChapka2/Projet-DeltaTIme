using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Hex : MonoBehaviour
{
    [SerializeField] protected Vector2Int relativeCoords;
    public List<Grabbable> grabbablesOnThisHex = new List<Grabbable>();
    public Vector3 boundsCenter;

    private void Awake()
    {
        boundsCenter = GetComponent<MeshRenderer>().bounds.center;
    }

    public virtual void Tick()
    {
        grabbablesOnThisHex.First().Tick();
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
