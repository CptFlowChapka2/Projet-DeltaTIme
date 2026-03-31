using System;
using System.Linq;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject currentHoveredHex = null;
    public Grabbable currentGrabbedObject = null;

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
            currentGrabbedObject = hex.grabbablesOnThisHex.Last();
            hex.RemoveGrabbable(currentGrabbedObject);
        }
        else if (hex.grabbablesOnThisHex.Count == 0 || !(currentGrabbedObject is Machine && hex.grabbablesOnThisHex.Last() is Ingredient))
        {
            hex.AddGrabbable(currentGrabbedObject);
            currentGrabbedObject.transform.position = hex.transform.position + new Vector3(0, 0.5f, 0);
            currentGrabbedObject = null;
        }
    }
}
