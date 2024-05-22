using Cysharp.Threading.Tasks;
using MageVsMonsters.Managers;
using MageVsMonsters.Models;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Views
{
    public class FirePointView : BaseView<FirePointModel>
    {
        private CreatureView _creatureView;

        protected override async UniTask Initialize()
        {
            _creatureView = this.gameObject.GetComponentInParent<CreatureView>();
            await UniTask.WaitUntil(() => _creatureView != null &&
                                          _creatureView.Model != null);
            this.Model = new FirePointModel(_creatureView.Model);

            await UniTask.WaitUntil(() => ProjectilesManager.Instance != null &&
                                          ProjectilesManager.Instance.IsInitialized);
            ProjectilesManager.Instance.Register(this);
        }
        protected override async UniTask UnInitialize()
        {
            ProjectilesManager.Instance.UnRegister(this);
        }

        protected override async UniTask Subscribe()
        {
        }
        protected override async UniTask UnSubscribe()
        {
        }
    }
}
