using Cysharp.Threading.Tasks;
using MageVsMonsters.Extensions;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class PlayerManager : BaseManager<PlayerManager>
    {
        [SerializeField]
        private PlayerView _playerViewPrefab;

        public PlayerView PlayerViewInstance { get; private set; }

        protected override async UniTask Initialize()
        {
            await UniTask.WaitUntil(() => SpawnPointsManager.Instance != null &&
                                          SpawnPointsManager.Instance.IsInitialized);

            var playerModel = new PlayerModel(100);
            PlayerViewInstance = (PlayerView)this.InstantiateElement(playerModel, _playerViewPrefab, this.gameObject.transform);
            var spawnPointComponent = SpawnPointsManager.Instance.GetSpawnPointComponent<PlayerView>();
            // TODO: move to view
            this.InvokeActionAfterFrames(() =>
            {
                PlayerViewInstance.transform.position = spawnPointComponent.transform.position;
            }, 1);

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
