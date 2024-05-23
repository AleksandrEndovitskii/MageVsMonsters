using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.SpawnPoints;
using MageVsMonsters.Views;
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
            SpawnPointComponent spawnPointComponentInstance = null;

            // TODO: bad implementation - reimplement in future
            if (typeof(T) == typeof(PlayerView))
            {
                spawnPointComponentInstance = _spawnPointComponentInstances[0];
            }
            if (typeof(T) == typeof(EnemyView))
            {
                spawnPointComponentInstance = _spawnPointComponentInstances[1];
            }

            return spawnPointComponentInstance;
        }
        public Vector3 GetSpawnPointPosition<T>() where T : CreatureView
        {
            var spawnPointPosition = Vector3.zero;

            // TODO: bad implementation - reimplement in future
            if (typeof(T) == typeof(PlayerView))
            {
                spawnPointPosition = new Vector3(30, 0, 30);
            }
            if (typeof(T) == typeof(EnemyView))
            {
                spawnPointPosition = GetRandomVector3ExcludingRange(
                    new Vector3(5, 0, 5),
                    new Vector3(55, 0, 55),
                    new Vector3(20, 0, 20),
                    new Vector3(40, 0, 40));
            }

            return spawnPointPosition;
        }

        public static Vector3 GetRandomVector3ExcludingRange(Vector3 minInclusive, Vector3 maxInclusive, Vector3 excludeMinInclusive, Vector3 excludeMaxInclusive)
        {
            Vector3 randomVector;
            while (true)
            {
                // Generate random components for the Vector3
                float x = UnityEngine.Random.Range(minInclusive.x, maxInclusive.x);
                float y = UnityEngine.Random.Range(minInclusive.y, maxInclusive.y);
                float z = UnityEngine.Random.Range(minInclusive.z, maxInclusive.z);

                randomVector = new Vector3(x, y, z);

                // Check if the generated vector is outside the exclusion range
                if (randomVector.x < excludeMinInclusive.x || randomVector.x > excludeMaxInclusive.x ||
                    randomVector.y < excludeMinInclusive.y || randomVector.y > excludeMaxInclusive.y ||
                    randomVector.z < excludeMinInclusive.z || randomVector.z > excludeMaxInclusive.z)
                {
                    break;
                }
            }

            return randomVector;
        }
    }
}
