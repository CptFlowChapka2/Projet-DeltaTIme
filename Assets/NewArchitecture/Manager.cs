using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    public GameManager gameManager;
    protected List<Doer> allDoers =  new List<Doer>();

    protected void GetAllDoersOnGameObject()
    {
        allDoers = GetComponents<Doer>().ToList();
    }

    protected void InitializeDoer<T>(out T appropriateDoer) where T : Doer
    {
        appropriateDoer = (T)allDoers.Where(x => x.GetType() == typeof(T)).First(); // Cherche la bonne classe étendue de Doer
        appropriateDoer.Manager = this;
    }

    public void ForceDoerToGetUsefullData<T>() where T:Doer
    {
        if (typeof(T).IsSubclassOf(typeof(Doer)))
        {
            allDoers.Where(x=> x.GetType() == typeof(T)).ToList().ForEach(x=>x.GetAllUsefulParameters());
            return;
        }
        allDoers.ForEach(x=>x.GetAllUsefulParameters());
    }
    public void ForceDoerToSetUsefullData<T>() where T:Doer
    {
        if (typeof(T).IsSubclassOf(typeof(Doer)))
        {
            allDoers.Where(x=> x.GetType() == typeof(T)).ToList().ForEach(x=>x.SetAllUsedParameters());
            return;
        }
        allDoers.ForEach(x=>x.SetAllUsedParameters());
    }
}
