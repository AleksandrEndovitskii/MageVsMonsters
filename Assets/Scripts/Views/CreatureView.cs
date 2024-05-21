using Cysharp.Threading.Tasks;
using MageVsMonsters.Models;

namespace MageVsMonsters.Views
{
    public class CreatureView : BaseView<CreatureModel>
    {
        protected override async UniTask Subscribe()
        {
            await base.Subscribe();

            await UniTask.WaitUntil(() => this.Model != null);
            this.Model.IsAliveChanged += Model_IsAliveChanged;
            Model_IsAliveChanged(this.Model.IsAlive);
        }
        protected override async UniTask UnSubscribe()
        {
            if (this.Model != null)
            {
                this.Model.IsAliveChanged -= Model_IsAliveChanged;
            }

            await base.UnSubscribe();
        }

        private void Model_IsAliveChanged(bool isAlive)
        {
            if (!isAlive)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
