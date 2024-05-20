using Cysharp.Threading.Tasks;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
using UnityEngine;

namespace MageVsMonsters.Managers
{
    public class CharactersManager : BaseManager<CharactersManager>
    {
        [SerializeField]
        private PlayerView _playerViewPrefab;

        public PlayerView PlayerViewInstance { get; private set; }

        protected override async UniTask Initialize()
        {
            InstantiatePlayer();

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

        private void InstantiatePlayer()
        {
            var playerModel = new PlayerModel();
            PlayerViewInstance = (PlayerView)this.InstantiateElement(playerModel, _playerViewPrefab);
        }
    }
}
