using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.BaseComponents;
using MageVsMonsters.Managers;
using MageVsMonsters.Views;
using UnityEngine;
using UnityEngine.AI;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Components.NavigationComponents
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerFollowingComponent : BaseComponent
    {
        private NavMeshAgent _navMeshAgent;
        private PlayerView _target;

        protected override async UniTask Initialize()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            await UniTask.WaitUntil(() => PlayersManager.Instance != null &&
                                          PlayersManager.Instance.IsInitialized);
            _target = FindFirstObjectByType<PlayerView>();
        }
        protected override async UniTask UnInitialize()
        {
        }

        protected override async UniTask Subscribe()
        {
        }
        protected override async UniTask UnSubscribe()
        {
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            _navMeshAgent.SetDestination(_target.transform.position);
        }
    }
}
