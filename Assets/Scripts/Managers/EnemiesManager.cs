using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
using UnityEngine;

namespace MageVsMonsters.Managers
{
    public class EnemiesManager : BaseManager<EnemiesManager>
    {
        [SerializeField]
        private EnemyView _enemyViewPrefab;
        [SerializeField]
        private int _enemiesCount;

        public List<EnemyView> EnemyViewInstances = new List<EnemyView>();

        protected override async UniTask Initialize()
        {
            InstantiateEnemies();

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

        private void InstantiateEnemies()
        {
            for (int i = 0; i < _enemiesCount; i++)
            {
                var enemyModel = new EnemyModel();
                var enemyViewInstance = (EnemyView)this.InstantiateElement(enemyModel, _enemyViewPrefab);
                EnemyViewInstances.Add(enemyViewInstance);
            }
        }
    }
}
