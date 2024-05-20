using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.BaseComponents;
using MageVsMonsters.Helpers;
using MageVsMonsters.Managers;
using MageVsMonsters.Views;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Components.CollisionDetectionComponents
{
    [RequireComponent(typeof(Collider))]
    public class CollisionDetectionComponent : BaseComponent
    {
        protected override async UniTask Initialize()
        {
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

        private void OnCollisionEnter(Collision otherCollision)
        {
            if (CollisionHandlingManager.Instance == null)
            {
                return;
            }

            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                                $"\nthisCollision={this.gameObject}" +
                                $"\n{nameof(otherCollision)} == {otherCollision.gameObject}");

            var view1 = this.gameObject.GetComponentInParent<IBaseView>();
            var view2 = otherCollision.gameObject.GetComponentInParent<IBaseView>();
            CollisionHandlingManager.Instance.HandleOnCollisionEnter(view1, view2);
        }
        private void OnCollisionExit(Collision otherCollision)
        {
            if (CollisionHandlingManager.Instance == null)
            {
                return;
            }

            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                            $"\nthisCollision={this.gameObject}" +
                            $"\n{nameof(otherCollision)} == {otherCollision.gameObject}");

            var view1 = this.gameObject.GetComponentInParent<IBaseView>();
            var view2 = otherCollision.gameObject.GetComponentInParent<IBaseView>();
            CollisionHandlingManager.Instance.HandleOnCollisionExit(view1, view2);
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (CollisionHandlingManager.Instance == null)
            {
                return;
            }

            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                            $"\nthisCollider={this.gameObject}" +
                            $"\n{nameof(otherCollider)} == {otherCollider.gameObject}");

            var view1 = this.gameObject.GetComponentInParent<IBaseView>();
            var view2 = otherCollider.gameObject.GetComponentInParent<IBaseView>();
            CollisionHandlingManager.Instance.HandleOnTriggerEnter(view1, view2);
        }
        private void OnTriggerExit(Collider otherCollider)
        {
            if (CollisionHandlingManager.Instance == null)
            {
                return;
            }

            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                            $"\nthisCollider={this.gameObject}" +
                            $"\n{nameof(otherCollider)} == {otherCollider.gameObject}");

            var view1 = this.gameObject.GetComponentInParent<IBaseView>();
            var view2 = otherCollider.gameObject.GetComponentInParent<IBaseView>();
            CollisionHandlingManager.Instance.HandleOnTriggerExit(view1, view2);
        }
    }
}
