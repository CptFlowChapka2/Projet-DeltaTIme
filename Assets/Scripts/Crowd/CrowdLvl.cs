using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrowdLvl : MonoBehaviour
{
    private List<IndividualCrowdScript> _indidualCrowdScripts=new List<IndividualCrowdScript>();
    public int[] currentLvl=new []{0,0,0};


    private void Start()
    {
        foreach (var child in transform.GetComponentsInChildren<IndividualCrowdScript>())
        {
            _indidualCrowdScripts.Add(child);
        }
        
        ChangeAllLvl(0);
    }

    public void ReceivePlayerOnBeat(int playerI,bool onOff,Vector2Int ignore)
    {
        Debug.Log("ReceivedPLayerOnBeat  "+onOff);

        CrowdTags leftRight = playerI switch
        {
            1 => CrowdTags.Left,
            2 => CrowdTags.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(playerI), playerI, null)
        };
        currentLvl[playerI] = onOff switch {
            true => currentLvl[playerI]+1,
            false => currentLvl[playerI]-1
        };

        for (int i = 0; i < currentLvl.Length-1; i++)
        {
            currentLvl[i] = Math.Clamp(currentLvl[i], 0, 2);
        }
        currentLvl[0] = currentLvl[1] + currentLvl[2];
        ChangeAllLvlByTags(TagRestriction.ContainAny,new [] {leftRight},currentLvl[playerI]);
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



