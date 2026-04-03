using System;
using System.Linq;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject currentHoveredHex = null;
    public Grabbable currentGrabbedObject = null;
    [NonSerialized] public Player player;

    private void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up * 3f, out RaycastHit hit))
        {
            currentHoveredHex = hit.collider.gameObject;
        }

        if (currentGrabbedObject)
        {
            currentGrabbedObject.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }

    public void OnGrabRelease()
    {
        Hex hex = currentHoveredHex.GetComponentInParent<Hex>();
        if (!currentGrabbedObject && hex.grabbablesOnThisHex.Count != 0)
        {
            if(player.isGrabbed && hex.grabbablesOnThisHex.Last() is Player)return;
            GrabOn(hex);
        }
        else if (currentGrabbedObject &&(hex.grabbablesOnThisHex.Count == 0 || !(!(currentGrabbedObject is Ingredient) && hex.grabbablesOnThisHex.Last() is Ingredient)))
        {
            ReleaseOn(hex);
        }
    }

    private void ReleaseOn(Hex hex)
    {
        hex.AddGrabbable(currentGrabbedObject);
        currentGrabbedObject.isGrabbed = false;
        currentGrabbedObject.transform.position = hex.transform.position + new Vector3(0, 0.5f, 0);
        currentGrabbedObject = null;
    }

    private void GrabOn(Hex hex)
    {
        currentGrabbedObject = hex.grabbablesOnThisHex.Last();
        currentGrabbedObject.isGrabbed = true;
        currentGrabbedObject.isActive = false;
        currentGrabbedObject.beforeGrabVariant = currentGrabbedObject.ActualVariant;
        currentGrabbedObject.ActualVariant =currentGrabbedObject.variants.Length-1;
        hex.RemoveGrabbable(currentGrabbedObject);
    }
}
