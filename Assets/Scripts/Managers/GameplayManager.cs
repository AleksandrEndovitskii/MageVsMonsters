using Cysharp.Threading.Tasks;
using MageVsMonsters.Views;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class GameplayManager : BaseManager<GameplayManager>
    {
        [SerializeField]
        private KeyCode _castSpellKey = KeyCode.X;
        [SerializeField]
        private KeyCode _switchToNextSpellKey = KeyCode.E;
        [SerializeField]
        private KeyCode _switchToPreviousSpellKey = KeyCode.Q;

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
            await UniTask.WaitUntil(() => InputManager.Instance != null &&
                                          InputManager.Instance.IsInitialized);
            InputManager.Instance.KeyPressed += InputManager_KeyPressed;

            await UniTask.WaitUntil(() => CollisionHandlingManager.Instance != null &&
                                          CollisionHandlingManager.Instance.IsInitialized);
            CollisionHandlingManager.Instance.TriggerEnter += CollisionHandlingManager_TriggerEnter;
            CollisionHandlingManager.Instance.CollisionEnter += CollisionHandlingManager_CollisionEnter;
        }
        protected override async UniTask UnSubscribe()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.KeyPressed -= InputManager_KeyPressed;
            }

            if (CollisionHandlingManager.Instance != null)
            {
                CollisionHandlingManager.Instance.TriggerEnter -= CollisionHandlingManager_TriggerEnter;
                CollisionHandlingManager.Instance.CollisionEnter -= CollisionHandlingManager_CollisionEnter;
            }
        }

        private void InputManager_KeyPressed(KeyCode keyCode)
        {
            HandleKeyPressed(keyCode);
        }

        private void TryHandleProjectileCreatureCollisionEnter(IBaseView baseView1, IBaseView baseView2)
        {
            var projectileView = baseView1 as SpellView;
            var enemyView = baseView2 as EnemyView;

            if (projectileView == null ||
                enemyView == null)
            {
                return;
            }

            enemyView.Model.DoDamage(projectileView.Model.Damage);
            Destroy(projectileView.gameObject);
        }
        private void TryHandleEnemyPlayerCollisionEnter(IBaseView baseView1, IBaseView baseView2)
        {
            var enemyView = baseView1 as EnemyView;
            var playerView = baseView2 as PlayerView;

            if (enemyView == null ||
                playerView == null)
            {
                return;
            }

            playerView.Model.DoDamage(enemyView.Model.Damage);
        }

        private void HandleKeyPressed(KeyCode keyCode)
        {
            if (keyCode == _castSpellKey)
            {
                // TODO: temp solution - cast spell from the first player
                var playerView = PlayersManager.Instance.Instances[0];
                SpellsManager.Instance.CastSpell(playerView, null);
            }
            if (keyCode == _switchToNextSpellKey)
            {
                SpellsManager.Instance.SwitchToNextSpell();
            }
            if (keyCode == _switchToPreviousSpellKey)
            {
                SpellsManager.Instance.SwitchToPreviousSpell();
            }
        }

        private void CollisionHandlingManager_TriggerEnter(IBaseView baseView1, IBaseView baseView2)
        {
            TryHandleProjectileCreatureCollisionEnter(baseView1, baseView2);
            TryHandleEnemyPlayerCollisionEnter(baseView1, baseView2);
        }
        private void CollisionHandlingManager_CollisionEnter(IBaseView baseView1, IBaseView baseView2)
        {
            TryHandleProjectileCreatureCollisionEnter(baseView1, baseView2);
            TryHandleEnemyPlayerCollisionEnter(baseView1, baseView2);
        }
    }
}
