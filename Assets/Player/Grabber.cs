using System;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject currentHoveredHex = null;

    private void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up * 3f, out RaycastHit hit))
        {
            currentHoveredHex = hit.collider.gameObject;
        }
    }
}
