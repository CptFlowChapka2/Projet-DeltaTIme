using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
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
}
