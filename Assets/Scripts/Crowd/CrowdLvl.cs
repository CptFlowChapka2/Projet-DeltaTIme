using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrowdLvl : MonoBehaviour
{
    private List<IndividualCrowdScript> _indidualCrowdScripts=new List<IndividualCrowdScript>();
    public int currentSumLvl;
    public int currentP1Lvl;
    public int currentP2Lvl;


    private void Start()
    {
        foreach (var child in transform.GetComponentsInChildren<IndividualCrowdScript>())
        {
            _indidualCrowdScripts.Add(child);
        }
        
        ChangeAllLvl(0);
    }

    public void ReceivePlayerOnBeat(int playerI)
    {
        switch (playerI)
        {
            case 1:
                ChangeAllLvlByTags(TagRestriction.ContainAny,new [] { CrowdTags.Left},currentP1Lvl);
                break;
            
            case 2:
                ChangeAllLvlByTags(TagRestriction.ContainAny,new [] { CrowdTags.Right},currentP2Lvl);
                
                break;
        }
    }
    
    private void ChangeAllLvl(int newLvl)
    {
        _indidualCrowdScripts.ForEach(x=>x.TryChangeLvl(newLvl));
    }

    private void ChangeAllLvlByTags(TagRestriction tagRestriction,CrowdTags[] tagToFilter,int newLvl)
    {
        
        //groso merdo on choisis comment on filtre les tag
        switch (tagRestriction)
        {
            case TagRestriction.ContainAny:
                _indidualCrowdScripts.FindAll(x=>tagToFilter.Any(compareTags => x.thisTags.Contains(compareTags)))
                    .ForEach(x=>x.TryChangeLvl(newLvl));
                //puis  on cherche si dans nos tagfiltre y'en as qui sont dans les tag d'un object .
                break;
            case TagRestriction.ContainAll:
                _indidualCrowdScripts.FindAll(x=>tagToFilter.All(compareTags => x.thisTags.Contains(compareTags)))
                    .ForEach(x=>x.TryChangeLvl(newLvl));
                break;
            case TagRestriction.DoNotContainAny:
                _indidualCrowdScripts.FindAll(x=>tagToFilter.Any(compareTags => !x.thisTags.Contains(compareTags)))
                    .ForEach(x=>x.TryChangeLvl(newLvl));
                break;
            case TagRestriction.DoNotContainAll:
                _indidualCrowdScripts.FindAll(x=>tagToFilter.All(compareTags =>!x.thisTags.Contains(compareTags)))
                    .ForEach(x=>x.TryChangeLvl(newLvl));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tagRestriction), tagRestriction, null);
        }
    }
    private enum TagRestriction
    {
        ContainAny,
        ContainAll,
        DoNotContainAny,
        DoNotContainAll
    }
}



