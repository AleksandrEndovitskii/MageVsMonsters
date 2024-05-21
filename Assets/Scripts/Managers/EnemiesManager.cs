using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Extensions;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class EnemiesManager : BaseManager<EnemiesManager>
    {
        [SerializeField]
        private EnemyView _enemyViewPrefab;
        [SerializeField]
        private int _enemiesCount = 1;

        public List<EnemyView> EnemyViewInstances = new List<EnemyView>();

        protected override async UniTask Initialize()
        {
            await UniTask.WaitUntil(() => SpawnPointsManager.Instance != null &&
                                          SpawnPointsManager.Instance.IsInitialized);

            for (int i = 0; i < _enemiesCount; i++)
            {
                var enemyModel = new EnemyModel(100);
                var enemyViewInstance = (EnemyView)this.InstantiateElement(enemyModel, _enemyViewPrefab, this.gameObject.transform);
                var spawnPointComponent = SpawnPointsManager.Instance.GetSpawnPointComponent<EnemyView>();
                // TODO: move to view
                this.InvokeActionAfterFrames(() =>
                {
                    enemyViewInstance.transform.position = spawnPointComponent.transform.position;
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
    }
}
