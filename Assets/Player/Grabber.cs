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
    [SerializeField] private GameObject shadow;
    private float offsetHeight = 0.6f;

    private void Update()
    {
        ShadowPlacement();
        if (Physics.Raycast(transform.position, -transform.up * 3f, out RaycastHit hit,Mathf.Infinity,-1,QueryTriggerInteraction.Ignore))
        {
            if (currentHoveredHex)
            {
                currentHoveredHex.isHovered = false;
                currentHoveredHex.isPlayerHovering[player.playerID] = false;
            }
            
            currentHoveredHexGO = hit.collider.gameObject;
            currentHoveredHex = currentHoveredHexGO.GetComponentInParent<Hex>();
            
            if (currentHoveredHex is not null)
            {
                currentHoveredHex.isHovered = true;
                currentHoveredHex.isPlayerHovering[player.playerID] = true;
            }
        }

        if (currentGrabbedObject)
        {
            currentGrabbedObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    private void ShadowPlacement()
    {
        if (player.isGrabbed)
        {
            offsetHeight = 1.2f;
        }
        else
        {
            offsetHeight = 0.6f;
        }

        shadow.transform.position = new Vector3(transform.position.x, transform.position.y - offsetHeight, transform.position.z);
    }

    public void OnGrabRelease()
    {
        if (currentHoveredHex is HalfBlockerHex) return;
        if (!currentGrabbedObject && currentHoveredHex.grabbablesOnThisHex.Count != 0)
        {
            if(player.isGrabbed && currentHoveredHex.grabbablesOnThisHex.Last() is Player) return;
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
