using System;
using UnityEngine;

public class LightStateChecker : MonoBehaviour
{
    public TileState tileState;
    private MeshCollider collider;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        collider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        DoActionsRegardingOfState();
    }

    private void DoActionsRegardingOfState()
    {
        switch (tileState)
        {
            case TileState.Invisible:
                collider.enabled = false;
                meshRenderer.enabled = false;
                break;
            case TileState.Active:
                collider.enabled = true;
                meshRenderer.enabled = true;
                break;
            case TileState.Unused:
                Destroy(gameObject);
                break;
            case TileState.ActiveNextBeat:
                break;
            default:
                break;
        }
    }
}
