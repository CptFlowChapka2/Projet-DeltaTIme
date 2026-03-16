using System;
using UnityEngine;

public class UpdateTileMaterial : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    private TileId tileId;
    
    [SerializeField] private Color baseColor;
    [SerializeField] private Color damagingColor;
    [SerializeField] private Color warningColor;

    private void Start()
    {
        tileId = GetComponent<TileId>();
        tileId.danceFloorManager.gameManager.onBeat.AddListener(ListenForOnBeat);
    }

    public void ListenForOnBeat()
    {
        if (tileId.allProjectileOnThisTile.Count > 0)
        {
            meshRenderer.material.color = damagingColor;
        }
        else if (tileId.allProjectileOnThisTileNextBeat.Count > 0)
        {
            meshRenderer.material.color = warningColor;
        }
        else
        {
            meshRenderer.material.color = baseColor;
        }
    }
}
