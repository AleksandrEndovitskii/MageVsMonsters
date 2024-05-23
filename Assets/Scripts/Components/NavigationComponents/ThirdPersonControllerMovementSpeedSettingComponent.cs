using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.BaseComponents;
using MageVsMonsters.Views;
using StarterAssets;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Components.NavigationComponents
{
    [RequireComponent(typeof(ThirdPersonController))]
    [RequireComponent(typeof(CreatureView))]
    public class ThirdPersonControllerMovementSpeedSettingComponent : BaseComponent
    {
        private ThirdPersonController _thirdPersonController;
        private CreatureView _creatureView;

        protected override async UniTask Initialize()
        {
            _thirdPersonController = GetComponent<ThirdPersonController>();
            _creatureView = GetComponent<CreatureView>();

            await UniTask.WaitUntil(() => _creatureView != null &&
                                          _creatureView.Model != null);
            _thirdPersonController.MoveSpeed = _creatureView.Model.MovementSpeed;
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
