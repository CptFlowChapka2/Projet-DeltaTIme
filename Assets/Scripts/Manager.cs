using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Manager : MonoBehaviour
    {
        protected List<DoActions> allDoActions =  new List<DoActions>();

        protected void GetAllActionsOnGameObject()
        {
            allDoActions = GetComponents<DoActions>().ToList();
        }

        protected void InitializeAction<T>(out T thing) where T : DoActions
        {
            thing = (T)allDoActions.Where(x => x.GetType() == typeof(T)).First(); // Cherche la bonne classe étendue de DoActions
            thing.Manager = this;
        }
    }
}