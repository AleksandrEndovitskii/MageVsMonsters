using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Extensions;
using MageVsMonsters.Helpers;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
using Newtonsoft.Json;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public abstract class CreaturesManager<T> : BaseManager<CreaturesManager<T>> where T : CreatureView
    {
        [SerializeField]
        private T _viewPrefab;
        [SerializeField]
        private int _instancesCount = 1;

        public List<T> Instances = new List<T>();

        protected override async UniTask Initialize()
        {
            await UniTask.WaitUntil(() => SpawnPointsManager.Instance != null &&
                                          SpawnPointsManager.Instance.IsInitialized);

            for (int i = 0; i < _instancesCount; i++)
            {
                var creatureModel = CreateModel();
                var creatureViewInstance = (T)this.InstantiateElement(creatureModel, _viewPrefab, this.gameObject.transform);
                var spawnPointComponent = SpawnPointsManager.Instance.GetSpawnPointComponent<T>();
                // TODO: move to view
                this.InvokeActionAfterFrames(() =>
                {
                    creatureViewInstance.transform.position = spawnPointComponent.transform.position;
                }, 1);
            }

            IsInitialized = true;
        }
        protected override async UniTask UnInitialize()
        {
            IsInitialized = false;
        }
        
        protected override async UniTask Subscribe()
        {
        }
        protected override async UniTask UnSubscribe()
        {
        }

        public void Register(T creatureViewInstance)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}"
                      + $"\n{nameof(creatureViewInstance)}.{nameof(creatureViewInstance.Model)} == {JsonConvert.SerializeObject(creatureViewInstance.Model)}");

            Instances.Add(creatureViewInstance);
        }
        public void UnRegister(T creatureViewInstance)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}"
                      + $"\n{nameof(creatureViewInstance)}.{nameof(creatureViewInstance.Model)} == {JsonConvert.SerializeObject(creatureViewInstance.Model)}");

            Instances.Remove(creatureViewInstance);
        }

        protected abstract CreatureModel CreateModel();
    }
}
