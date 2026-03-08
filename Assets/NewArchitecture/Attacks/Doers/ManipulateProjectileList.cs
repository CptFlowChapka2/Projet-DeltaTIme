using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ManipulateProjectileList : Doer
{
    
    private Vector3 defaultPosition;
    private List<ProjectileID> allProjectileIds = new List<ProjectileID>();
    private List<ProjectileID> currentlyActiveProjectileIds = new List<ProjectileID>();
    
    private AttacksManager attacksManager;
    private void Awake()
    {
        attacksManager = (AttacksManager)manager;
    }

    private void Start()
    {
        GetAllUsefulParameters();
        SpawnAllProjectile();
        SetAllUsedParameters();
    }

    public override void GetAllUsefulParameters()
    {
        defaultPosition = attacksManager.inactiveProjectileIdPosition;
        allProjectileIds = attacksManager.allProjectileIds;
        currentlyActiveProjectileIds = attacksManager.currentlyActiveProjectileIds;
    }

    public override void SetAllUsedParameters()
    {
        attacksManager.allProjectileIds = allProjectileIds;
         
    }

    private void SpawnAllProjectile()
    {

        int maxNumberOfProjectileId = attacksManager.maxNumberOfProjectileId;
        GameObject prefab = attacksManager.projectileIdPrefab;
        for (int i = 0; i < maxNumberOfProjectileId; i++)
        {
            GameObject justSpawnedActivatedTile = Instantiate(prefab,defaultPosition,Quaternion.identity);
            allProjectileIds.Add(justSpawnedActivatedTile.GetComponent<ProjectileID>());
        }
        
        allProjectileIds.ForEach(x=>x.attacksManager=attacksManager);
    }

    public void RemoveProjectileFromUnusedList(ProjectileID projectileID)
    {
        currentlyActiveProjectileIds.Remove(projectileID);
    }
    public void AddProjectileFromUnusedList(ProjectileID projectileID)
    {
        currentlyActiveProjectileIds.Add(projectileID);
    }
}
