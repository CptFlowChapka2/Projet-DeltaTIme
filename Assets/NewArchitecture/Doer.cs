using UnityEngine;

public abstract class Doer : MonoBehaviour
{
    protected Manager manager;

    public Manager Manager
    {
        get => manager;
        set => manager = value;
    }

    protected virtual void GetAllUsefulParameters()
    {
        
    }
    
    protected virtual void SetAllUsedParameters()
    {
        
    }
}
