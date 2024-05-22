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
        private int _initialInstancesCount = 0;

        public List<T> Instances = new List<T>();

        protected override async UniTask Initialize()
        {
            await UniTask.WaitUntil(() => SpawnPointsManager.Instance != null &&
                                          SpawnPointsManager.Instance.IsInitialized);

            for (int i = 0; i < _initialInstancesCount; i++)
            {
                var model = CreateModel();
                var instance = (T)this.InstantiateElement(model, _viewPrefab, this.gameObject.transform);
                // TODO: move to view
                this.InvokeActionAfterFrames(() =>
                {
                    var spawnPointComponent = SpawnPointsManager.Instance.GetSpawnPointComponent<T>();
                    instance.transform.position = spawnPointComponent.transform.position;
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

        public void Register(T instance)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}"
                      + $"\n{nameof(instance)}.{nameof(instance.Model)} == {JsonConvert.SerializeObject(instance.Model)}");

            Instances.Add(instance);
        }
        public void UnRegister(T instance)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}"
                      + $"\n{nameof(instance)}.{nameof(instance.Model)} == {JsonConvert.SerializeObject(instance.Model)}");

            Instances.Remove(instance);
        }

        protected abstract CreatureModel CreateModel();
    }
}
