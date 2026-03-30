using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AttacksManager : Manager
{
    private CalculateProjectileList _calculateProjectileList;
    private MoveProjectile moveProjectile;
    private SpawnProjectile spawnProjectile;
    public PatternBank patternBank;//todo : deprecated

    [Header("ManipulateProjectList")] 
    public GameObject projectileIdPrefab;
    public Vector3 inactiveProjectileIdPosition = new Vector3(0, 30, 0);
    public int maxNumberOfProjectileId = 5;
    public List<ProjectileID> allProjectileIds = new List<ProjectileID>();
    
    private void Awake()
    {
        GetAllDoersOnGameObject();
        InitializeDoer(out _calculateProjectileList);
        InitializeDoer(out moveProjectile);
        InitializeDoer(out spawnProjectile);
    }
    
    public bool[] GetPatternVariantForAnDir(Vector2Int dir, int variantID)
    {
        return patternBank.AllPatternes[dir].allLines[variantID-1];
    }
    
   
    
}
