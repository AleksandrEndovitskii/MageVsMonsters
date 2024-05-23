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
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public abstract class CreaturesManager<T> : BaseManager<CreaturesManager<T>> where T : CreatureView
    {
        [SerializeField]
        private int _initialInstancesCount = 0;

        public List<T> Instances = new List<T>();

        protected abstract string DefinitionPath { get; }
        protected abstract string PrefabsPath { get; }

        private List<CreatureDefinitionJsonObject> _definitionJsonObjects = new List<CreatureDefinitionJsonObject>();

        protected override async UniTask Initialize()
        {
            var definitionsJson = ResourcesHelper.LoadFromJsonInResources<string>(DefinitionPath);
            _definitionJsonObjects = JsonConvert.DeserializeObject<List<CreatureDefinitionJsonObject>>(definitionsJson);

            await UniTask.WaitUntil(() => SpawnPointsManager.Instance != null &&
                                          SpawnPointsManager.Instance.IsInitialized);

            for (int i = 0; i < _initialInstancesCount; i++)
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

        private CreatureModel GetRandomModel()
        {
            var randomValue = Random.Range(0, 100);
            var rarity = RarityHelper.GetRarity(randomValue);
            var result = GetModel(rarity);

            return result;
        }
        private CreatureModel GetModel(Rarity rarity)
        {
            var definitionJsonObject = _definitionJsonObjects.FirstOrDefault(em => em.Rarity == rarity);
            var model = new CreatureModel(definitionJsonObject);

            return model;
        }
        private CreatureModel CreateModel()
        {
            var creatureModel = GetRandomModel();
            return creatureModel;
        }
    }
}
