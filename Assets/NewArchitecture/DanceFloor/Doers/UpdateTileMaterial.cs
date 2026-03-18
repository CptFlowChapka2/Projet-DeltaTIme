using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateTileMaterial : MonoBehaviour
{
    [SerializeField] private MeshRenderer baseMeshRenderer;
    [SerializeField] private MeshRenderer signMeshRenderer;
    private TileId tileId;
    
    private List<Vector2Int> projectilesDirections = new List<Vector2Int>();
    private List<Vector2Int> signsDirections = new List<Vector2Int>();
    
    private Material[] projectileMaterials;
    private Material[] signsMaterials;
    
    // [SerializeField] private Color baseColor;
    // [SerializeField] private Color damagingColor;
    // [SerializeField] private Color warningColor;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material transparentMaterial;
    
    [Header("Projectile Materials")]
    [SerializeField] private Material onTileAllMaterial;
    [SerializeField] private Material onTileDownMaterial;
    [SerializeField] private Material onTileLeftMaterial;
    [SerializeField] private Material onTileLeftDownMaterial;
    [SerializeField] private Material onTileLeftRightMaterial;
    [SerializeField] private Material onTileLeftUpMaterial;
    [SerializeField] private Material onTileRightMaterial;
    [SerializeField] private Material onTileRightDownMaterial;
    [SerializeField] private Material onTileRightUpMaterial;
    [SerializeField] private Material onTileUpMaterial;
    [SerializeField] private Material onTileUpDownMaterial;
    
    [Header("Signs Materials")]
    [SerializeField] private Material onSignTileAllMaterial;
    [SerializeField] private Material onSignTileDownMaterial;
    [SerializeField] private Material onSignTileLeftMaterial;
    [SerializeField] private Material onSignTileLeftDownMaterial;
    [SerializeField] private Material onSignTileLeftRightMaterial;
    [SerializeField] private Material onSignTileLeftUpMaterial;
    [SerializeField] private Material onSignTileRightMaterial;
    [SerializeField] private Material onSignTileRightDownMaterial;
    [SerializeField] private Material onSignTileRightUpMaterial;
    [SerializeField] private Material onSignTileUpMaterial;
    [SerializeField] private Material onSignTileUpDownMaterial;

    private void Start()
    {
        tileId = GetComponent<TileId>();
        Debug.Assert(tileId.danceFloorManager.gameManager.onBeat is not null);
        tileId.danceFloorManager.gameManager.onBeat.AddListener(ListenForOnBeat);
        projectileMaterials = new Material[12]
        {
            onTileRightMaterial, onTileLeftMaterial, onTileUpMaterial, onTileDownMaterial,
            onTileLeftRightMaterial, onTileRightUpMaterial, onTileRightDownMaterial,
            onTileLeftUpMaterial, onTileLeftDownMaterial, onTileUpDownMaterial, onTileAllMaterial,
            baseMaterial
        };
        signsMaterials = new Material[12]
        {
            onSignTileLeftMaterial, onSignTileRightMaterial, onSignTileDownMaterial, onSignTileUpMaterial,
            onSignTileLeftRightMaterial, onSignTileLeftDownMaterial, onSignTileLeftUpMaterial,
            onSignTileRightDownMaterial, onSignTileRightUpMaterial, onSignTileUpDownMaterial, onSignTileAllMaterial,
            transparentMaterial
        };
    }

    public void ListenForOnBeat()
    {
        StoreProjectilesAndSignsDirections();
        ChooseMaterialRelativeToPatternPresence(signMeshRenderer, signsDirections, signsMaterials);
        ChooseMaterialRelativeToPatternPresence(baseMeshRenderer, projectilesDirections, projectileMaterials);
    }

    private void ChooseMaterialRelativeToPatternPresence(MeshRenderer renderer, List<Vector2Int> directions, Material[] materials)
    {
        switch (directions.Count)
        {
            case 0:
                renderer.material = materials[11];
                break;
            case 1:
                switch (directions[0].x)
                {
                    case 0:
                        break;
                    case -1:
                        renderer.material = materials[0];
                        break;
                    case 1:
                        renderer.material = materials[1];
                        break;
                }
                switch (directions[0].y)
                {
                    case 0:
                        break;
                    case -1:
                        renderer.material = materials[2];
                        break;
                    case 1:
                        renderer.material = materials[3];
                        break;
                }
                break;
            case 2:
                if (directions.Contains(new Vector2Int(-1, 0)) && 
                    directions.Contains(new Vector2Int(1, 0)))
                {
                    renderer.material = materials[4];
                }
                else if (directions.Contains(new Vector2Int(-1, 0)) &&
                         directions.Contains(new Vector2Int(0, -1)))
                {
                    renderer.material = materials[5];
                }
                else if (directions.Contains(new Vector2Int(-1, 0)) &&
                         directions.Contains(new Vector2Int(0, 1)))
                {
                    renderer.material = materials[6];
                }
                else if (directions.Contains(new Vector2Int(1, 0)) &&
                         directions.Contains(new Vector2Int(0, -1)))
                {
                    renderer.material = materials[7];
                }
                else if (directions.Contains(new Vector2Int(1, 0)) &&
                         directions.Contains(new Vector2Int(0, 1)))
                {
                    renderer.material = materials[8];
                }
                else if (directions.Contains(new Vector2Int(0, -1)) &&
                         directions.Contains(new Vector2Int(0, 1)))
                {
                    renderer.material = materials[9];
                }
                break;
            case 3:
                break;
            case 4:
                renderer.material = materials[10];
                break;
        }
    }

    private void StoreProjectilesAndSignsDirections()
    {
        projectilesDirections.Clear();
        signsDirections.Clear();

        foreach (ProjectileID projectileID in tileId.allProjectileOnThisTile)
        {
            projectilesDirections.Add(projectileID.directionOfMouvement);
        }

        foreach (ProjectileID projectileID in tileId.allProjectileOnThisTileNextBeat)
        {
            signsDirections.Add(projectileID.directionOfMouvement);
        }
    }
}
