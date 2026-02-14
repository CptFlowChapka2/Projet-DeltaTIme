using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrowdLvl : MonoBehaviour
{
    private List<IndidualCrowdScript> _indidualCrowdScripts=new List<IndidualCrowdScript>();
    public int currentLvl;


    private void Start()
    {
        foreach (var child in transform.GetComponentsInChildren<IndidualCrowdScript>())
        {
            _indidualCrowdScripts.Add(child);
        }
        
        ChangeAllLvl(0);
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



