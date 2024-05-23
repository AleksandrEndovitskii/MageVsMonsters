using Cysharp.Threading.Tasks;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class TerrainManager : BaseManager<TerrainManager>
    {
        [SerializeField]
        private Terrain _terrainPrefab;
        [SerializeField]
        private Terrain _terrainInstance;

        protected override async UniTask Initialize()
        {
            //_terrainInstance = Instantiate(_terrainPrefab);
            _terrainInstance = FindFirstObjectByType<Terrain>();

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
