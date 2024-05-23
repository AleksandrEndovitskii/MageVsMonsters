using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.BaseComponents;
using MageVsMonsters.Managers;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Components.NavigationComponents
{
    public class WorldBordersSetterComponent : BaseComponent
    {
        protected override async UniTask Initialize()
        {
        }
        protected override async UniTask UnInitialize()
        {
        }

        protected override async UniTask Subscribe()
        {
            await UniTask.WaitUntil(() => WorldManager.Instance != null &&
                                          WorldManager.Instance.IsInitialized);

            WorldManager.Instance.WorldSizeChanged += WorldManager_WorldSizeChanged;
            WorldManager_WorldSizeChanged(WorldManager.Instance.WorldSize);
            WorldManager.Instance.BorderSizeChanged += WorldManager_BorderSizeChanged;
            WorldManager_BorderSizeChanged(WorldManager.Instance.BorderSize);
        }
        protected override async UniTask UnSubscribe()
        {
            if (WorldManager.Instance != null)
            {
                WorldManager.Instance.WorldSizeChanged -= WorldManager_WorldSizeChanged;
                WorldManager.Instance.BorderSizeChanged -= WorldManager_BorderSizeChanged;
            }
        }

        private void WorldManager_WorldSizeChanged(Vector3 worldSize)
        {
            this.gameObject.transform.localScale = worldSize;
        }
        private void WorldManager_BorderSizeChanged(Vector3 borderSize)
        {
            this.gameObject.transform.localScale -= borderSize;
        }
    }
}
