using UnityEngine;

namespace MageVsMonsters.Components.BaseComponents
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        public void ReInitialize()
        {
            UnInitialize();
            Initialize();
        }

        protected abstract void Initialize();
        protected abstract void UnInitialize();

        protected abstract void Subscribe();
        protected abstract void UnSubscribe();
    }
}
