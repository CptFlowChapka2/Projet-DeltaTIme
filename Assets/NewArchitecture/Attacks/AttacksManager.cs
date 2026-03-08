using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AttacksManager : Manager
{
    private ManipulateProjectileList manipulateProjectileList;
    private MoveProjectile moveProjectile;
    private SpawnProjectile spawnProjectile;
    public PatternBank patternBank;

    [Header("ManipulateProjectList")] 
    public GameObject projectileIdPrefab;
    public Vector3 inactiveProjectileIdPosition = new Vector3(0, 30, 0);
    public int maxNumberOfProjectileId = 5;
    public List<ProjectileID> allProjectileIds = new List<ProjectileID>();
     public List<ProjectileID> currentlyActiveProjectileIds = new List<ProjectileID>();
     public List<ProjectileID> currentlyInactiveProjectileIds = new List<ProjectileID>();
    
    private void Awake()
    {
        GetAllDoersOnGameObject();
        InitializeDoer(out manipulateProjectileList);
        InitializeDoer(out moveProjectile);
        InitializeDoer(out spawnProjectile);
    }
    
    public bool[] GetPatternVariantForAnDir(Vector2Int dir, int variantID)
    {
        return patternBank.AllPatternes[dir].allLines[variantID-1];
    }
    
}
