using Cysharp.Threading.Tasks;
using MageVsMonsters.Managers;

namespace MageVsMonsters.Views
{
    public class EnemyView : CreatureView
    {
        protected override async UniTask Initialize()
        {
            await base.Initialize();

            EnemiesManager.Instance.Register(this);
        }
        protected override async UniTask UnInitialize()
        {
            EnemiesManager.Instance.UnRegister(this);

            await base.UnInitialize();
        }
    }
}
