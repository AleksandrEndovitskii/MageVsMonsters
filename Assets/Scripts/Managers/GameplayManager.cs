using Cysharp.Threading.Tasks;
using MageVsMonsters.Views;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class GameplayManager : BaseManager<GameplayManager>
    {
        protected override async UniTask Initialize()
        {
            IsInitialized = true;
        }

        protected override async UniTask UnInitialize()
        {
            IsInitialized = false;
        }

        protected override async UniTask Subscribe()
        {
            await UniTask.WaitUntil(() => CollisionHandlingManager.Instance != null &&
                                          CollisionHandlingManager.Instance.IsInitialized);
            CollisionHandlingManager.Instance.TriggerEnter += CollisionHandlingManager_TriggerEnter;
            CollisionHandlingManager.Instance.CollisionEnter += CollisionHandlingManager_CollisionEnter;
        }

        protected override async UniTask UnSubscribe()
        {
            if (CollisionHandlingManager.Instance != null)
            {
                CollisionHandlingManager.Instance.TriggerEnter -= CollisionHandlingManager_TriggerEnter;
                CollisionHandlingManager.Instance.CollisionEnter -= CollisionHandlingManager_CollisionEnter;
            }
        }

        private void TryHandleProjectileCreatureCollisionEnter(IBaseView baseView1, IBaseView baseView2)
        {
            var projectileView = baseView1 as ProjectileView;
            var enemyView = baseView2 as EnemyView;

            if (projectileView == null ||
                enemyView == null)
            {
                return;
            }

            enemyView.Model.Health -= projectileView.Model.Damage;
            Destroy(projectileView.gameObject);
        }

        private void CollisionHandlingManager_TriggerEnter(IBaseView baseView1, IBaseView baseView2)
        {
            TryHandleProjectileCreatureCollisionEnter(baseView1, baseView2);
        }
        private void CollisionHandlingManager_CollisionEnter(IBaseView baseView1, IBaseView baseView2)
        {
            TryHandleProjectileCreatureCollisionEnter(baseView1, baseView2);
        }
    }
}
