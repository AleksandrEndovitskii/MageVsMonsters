using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.SpawnPoints;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class SpawnPointsManager : BaseManager<SpawnPointsManager>
    {
#pragma warning disable 0649
        [SerializeField]
        private List<SpawnPointComponent> _spawnPointComponentPrefabs = new List<SpawnPointComponent>();
#pragma warning restore 0649

        [NonSerialized]
        private List<SpawnPointComponent> _spawnPointComponentInstances = new List<SpawnPointComponent>();

        protected override async UniTask Initialize()
        {
            foreach (var spawnPointComponentPrefab in _spawnPointComponentPrefabs)
            {
                var spawnPointComponentInstance = Instantiate(spawnPointComponentPrefab, this.gameObject.transform);
                _spawnPointComponentInstances.Add(spawnPointComponentInstance);
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

        public SpawnPointComponent GetSpawnPointComponent<T>()
        {
            return _spawnPointComponentInstances[0];
        }
    }
}
