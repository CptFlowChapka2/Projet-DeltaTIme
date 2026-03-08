using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AttacksManager : Manager
{
    private ManipulateProjectileList manipulateProjectileList;
    private MoveProjectile moveProjectile;

    [Header("ManipulateProjectList")] 
    public GameObject projectileIdPrefab;
    public Vector3 inactiveProjectileIdPosition = new Vector3(0, 30, 0);
    public int maxNumberOfProjectileId = 5;
    public List<ProjectileID> allProjectileIds = new List<ProjectileID>();
     public List<ProjectileID> currentlyActiveProjectileIds = new List<ProjectileID>();
    
    private void Awake()
    {
        GetAllDoersOnGameObject();
        InitializeDoer(out manipulateProjectileList);
        InitializeDoer(out moveProjectile);
    }
}
