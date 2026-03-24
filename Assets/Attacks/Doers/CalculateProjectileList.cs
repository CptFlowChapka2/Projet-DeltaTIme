using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CalculateProjectileList : Doer
{
    
    private Vector3 defaultPosition;
    private List<ProjectileID> allProjectileIds = new List<ProjectileID>();
    
    
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

    private void FixedUpdate()
    {
        SetAllUsedParameters();
    }

    public override void GetAllUsefulParameters()
    {
        defaultPosition = attacksManager.inactiveProjectileIdPosition;
        allProjectileIds = attacksManager.allProjectileIds;
       
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
    
}
