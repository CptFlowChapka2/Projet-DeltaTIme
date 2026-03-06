using UnityEngine;

namespace DefaultNamespace
{
    public abstract class DoActions : MonoBehaviour
    {
        protected Manager manager;

        public Manager Manager
        {
            get => manager;
            set => manager = value;
        }
    }
}