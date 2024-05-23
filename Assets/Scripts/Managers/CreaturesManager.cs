using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Extensions;
using MageVsMonsters.Helpers;
using MageVsMonsters.JsonObjects;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public abstract class CreaturesManager<T> : BaseManager<CreaturesManager<T>> where T : CreatureView
    {
        [SerializeField]
        protected int _initialInstancesCount = 0;

        public List<T> Instances = new List<T>();

        public event Action<int> InstancesCountChanged = delegate { };
        public int InstancesCount
        {
            get => _instancesCount;
            private set
            {
                if (value == _instancesCount)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_instancesCount} -> {value}");
                _instancesCount = value;

                InstancesCountChanged.Invoke(_instancesCount);
            }
        }
        private int _instancesCount;

        protected abstract string DefinitionPath { get; }
        protected abstract string PrefabsPath { get; }

        protected List<CreatureDefinitionJsonObject> _definitionJsonObjects = new List<CreatureDefinitionJsonObject>();

        protected override async UniTask Initialize()
        {
            var definitionsJson = ResourcesHelper.LoadFromJsonInResources<string>(DefinitionPath);
            _definitionJsonObjects = JsonConvert.DeserializeObject<List<CreatureDefinitionJsonObject>>(definitionsJson);

            await UniTask.WaitUntil(() => SpawnPointsManager.Instance != null &&
                                          SpawnPointsManager.Instance.IsInitialized);

            for (int i = 0; i < _initialInstancesCount; i++)
            {
                Spawn();
            }

            IsInitialized = true;
        }
        protected override async UniTask UnInitialize()
        {
            IsInitialized = false;
        }
        
        protected override async UniTask Subscribe()
        {
            InstancesCountChanged += OnInstancesCountChanged;
        }
        protected override async UniTask UnSubscribe()
        {
            InstancesCountChanged -= OnInstancesCountChanged;
        }

        public void Register(T instance)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}"
                      + $"\n{nameof(instance)}.{nameof(instance.Model)} == {JsonConvert.SerializeObject(instance.Model)}");

            Instances.Add(instance);
            InstancesCount++;
        }
        public void UnRegister(T instance)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}"
                      + $"\n{nameof(instance)}.{nameof(instance.Model)} == {JsonConvert.SerializeObject(instance.Model)}");

            Instances.Remove(instance);
            InstancesCount--;
        }

        protected void Spawn()
        {
            var model = CreateModel();
            var prefabPath = Path.Combine(PrefabsPath, model.Rarity.ToString());
            var prefab = Resources.Load<T>(prefabPath);
            var instance = (T)this.InstantiateElement(model, prefab, this.gameObject.transform);
            // TODO: move to view
            this.InvokeActionAfterFrames(() =>
            {
                var spawnPointComponent = SpawnPointsManager.Instance.GetSpawnPointComponent<T>();
                instance.transform.position = spawnPointComponent.transform.position;
            }, 1);
        }

        private CreatureModel CreateModel()
        {
            var creatureModel = GetRandomModel();
            return creatureModel;
        }
        private CreatureModel GetRandomModel()
        {
            var randomValue = Random.Range(0, 100);
            var rarity = RarityHelper.GetRarity(randomValue);
            var result = GetModel(rarity);

            return result;
        }
        protected virtual CreatureModel GetModel(Rarity rarity)
        {
            var definitionJsonObject = _definitionJsonObjects.FirstOrDefault(em => em.Rarity == rarity);
            var model = new CreatureModel(definitionJsonObject);

            return model;
        }

        protected virtual void OnInstancesCountChanged(int instancesCount)
        {
        }
    }
}
