using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ManipulateProjectileList : Doer
{
    
    private Vector3 defaultPosition;
    private List<ProjectileID> allProjectileIds = new List<ProjectileID>();
    private List<ProjectileID> currentlyActiveProjectileIds = new List<ProjectileID>();
     public List<ProjectileID> currentlyInactiveProjectileIds = new List<ProjectileID>();
    
    
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
        currentlyActiveProjectileIds = attacksManager.currentlyActiveProjectileIds;
        currentlyInactiveProjectileIds = attacksManager.currentlyInactiveProjectileIds;
    }

    public override void SetAllUsedParameters()
    {
        attacksManager.allProjectileIds = allProjectileIds;
        attacksManager.currentlyActiveProjectileIds =new List<ProjectileID>(currentlyActiveProjectileIds) ;
        attacksManager.currentlyInactiveProjectileIds =new List<ProjectileID>(currentlyInactiveProjectileIds) ;

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

        currentlyInactiveProjectileIds = new List<ProjectileID>(allProjectileIds);
        allProjectileIds.ForEach(x=>x.attacksManager=attacksManager);
    }

    public void SwitchProjectileToActiveList(ProjectileID projectileID)
    {
        if(!currentlyActiveProjectileIds.Contains(projectileID)) currentlyActiveProjectileIds.Add(projectileID);
        if(currentlyInactiveProjectileIds.Contains(projectileID)) currentlyActiveProjectileIds.RemoveAll(x=>projectileID);
        
        
        
        
    }
    public void SwitchProjectileToInactiveList(ProjectileID projectileID)
    {
        if(!currentlyActiveProjectileIds.Contains(projectileID)) currentlyActiveProjectileIds.RemoveAll(x=>projectileID);
        if(currentlyInactiveProjectileIds.Contains(projectileID)) currentlyActiveProjectileIds.Add(projectileID);
       
    }
}
