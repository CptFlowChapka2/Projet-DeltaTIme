using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject currentHoveredHexGO = null;
    public Hex currentHoveredHex = null;
    public Grabbable currentGrabbedObject = null;
    [NonSerialized] public Player player;

    private void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up * 3f, out RaycastHit hit,math.INFINITY,-1,QueryTriggerInteraction.Ignore))
        {
            if (currentHoveredHex) currentHoveredHex.isHovered = false;
            
            currentHoveredHexGO = hit.collider.gameObject;
            currentHoveredHex = currentHoveredHexGO.GetComponentInParent<Hex>();
            
            currentHoveredHex.isHovered= true;
        }

        if (currentGrabbedObject)
        {
            currentGrabbedObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    public void OnGrabRelease()
    {
        if (currentHoveredHex is HalfBlockerHex) return;
        if (!currentGrabbedObject && currentHoveredHex.grabbablesOnThisHex.Count != 0)
        {
            if(player.isGrabbed && currentHoveredHex.grabbablesOnThisHex.Last() is Player)return;
            GrabOn(currentHoveredHex);
        }
        else if (currentGrabbedObject &&(currentHoveredHex.grabbablesOnThisHex.Count == 0 || !(!(currentGrabbedObject is Ingredient) && currentHoveredHex.grabbablesOnThisHex.Last() is Ingredient)))
        {
            ReleaseOn(currentHoveredHex);
        }
    }

    private void ReleaseOn(Hex hex)
    {
        if(currentGrabbedObject is Machine && hex.grabbablesOnThisHex.Any(x=>x is Machine)) return;
        var grabOnHExWithoutMachine = new List<Grabbable>(hex.grabbablesOnThisHex);
        grabOnHExWithoutMachine.RemoveAll(x=>x is Machine);
        if(grabOnHExWithoutMachine.Count>=hex.maxNbrOfGrabbable) return;
        hex.AddGrabbable(currentGrabbedObject);

        if (hex.grabbablesOnThisHex.Contains(currentGrabbedObject))
        {
            currentGrabbedObject.isGrabbed = false;
            currentGrabbedObject.transform.position = hex.transform.position + new Vector3(0, 0.5f, 0);
            if(currentGrabbedObject.ActualVariant !=currentGrabbedObject.variants.Length-1)
            currentGrabbedObject.beforeGrabVariant = currentGrabbedObject.ActualVariant;
            currentGrabbedObject.ActualVariant =currentGrabbedObject.variants.Length-1;
            
            currentGrabbedObject = null;
        }
        
    }

    private void GrabOn(Hex hex)
    {
        currentGrabbedObject = hex.grabbablesOnThisHex.Last();
        if (currentGrabbedObject is Machine)
        {
            Machine currentMachine = currentGrabbedObject as Machine;
            currentMachine.loadingBarFrame.SetActive(false);
        }
        currentGrabbedObject.isGrabbed = true;
        currentGrabbedObject.isActive = false;
        
        hex.RemoveGrabbable(currentGrabbedObject);
    }
}
