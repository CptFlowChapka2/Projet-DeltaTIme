using UnityEngine;

public abstract class Doer : MonoBehaviour
{
    protected Manager manager;

    public Manager Manager
    {
        get => manager;
        set => manager = value;
    }

    public virtual void GetAllUsefulParameters()
    {
        
    }
    
    public virtual void SetAllUsedParameters()
    {
        
    }
}
