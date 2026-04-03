using UnityEngine;

public class EnergizerHex : Hex
{
    [SerializeField] private int speedBoost;

    public override void AddGrabbable(Grabbable grabbable)
    {
        base.AddGrabbable(grabbable);
        if (grabbable is Machine)
        {
            Machine machine = (Machine)grabbable;
            machine.possibleSpeedBoostByEnergizer = speedBoost;
        }
    }

    public override void RemoveGrabbable(Grabbable grabbable)
    {
        base.RemoveGrabbable(grabbable);
        if (grabbable is Machine)
        {
            Machine machine = (Machine)grabbable;
            machine.possibleSpeedBoostByEnergizer = 0;
        }
    }
}
