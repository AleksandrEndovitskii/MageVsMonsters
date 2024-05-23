using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.BaseComponents;
using MageVsMonsters.Views;
using UnityEngine;
using UnityEngine.AI;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Components.NavigationComponents
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CreatureView))]
    public class NavMeshAgentMovementSpeedSettingComponent : BaseComponent
    {
        private NavMeshAgent _navMeshAgent;
        private CreatureView _creatureView;

        protected override async UniTask Initialize()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _creatureView = GetComponent<CreatureView>();

            await UniTask.WaitUntil(() => _creatureView != null &&
                                          _creatureView.Model != null);
            _navMeshAgent.speed = _creatureView.Model.MovementSpeed;
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
    }
}
