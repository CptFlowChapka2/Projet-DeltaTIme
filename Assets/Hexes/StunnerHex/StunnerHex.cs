using System.Linq;
using UnityEngine;

public class StunnerHex : Hex
{
    [SerializeField] private int ticksToStun;
    private int ticks = 0;

    public override void Tick()
    {
        if(grabbablesOnThisHex.Count==0)return;
        if (grabbablesOnThisHex.First() is Player)
        {
            ticks++;
            if (ticks >= ticksToStun)
            {
                grabbablesOnThisHex.First().isActive = false;
            }
        }
        grabbablesOnThisHex?.First()?.Tick();
    }

    public override void RemoveGrabbable(Grabbable grabbable)
    {
        base.RemoveGrabbable(grabbable);
        if (grabbable is Player)
        {
            ticks = 0;
            grabbable.isActive = true;
        }
    }
}
