using Cysharp.Threading.Tasks;
using MageVsMonsters.Managers;

namespace MageVsMonsters.Views
{
    public class PlayerView : CreatureView
    {
        // TODO: copy/paste from EnemyView.cs
        protected override async UniTask Initialize()
        {
            await base.Initialize();

            PlayersManager.Instance.Register(this);
        }
        protected override async UniTask UnInitialize()
        {
            PlayersManager.Instance.UnRegister(this);

            await base.UnInitialize();
        }
    }
}
