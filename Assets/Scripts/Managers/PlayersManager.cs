using MageVsMonsters.Models;
using MageVsMonsters.Views;

namespace MageVsMonsters.Managers
{
    public class PlayersManager : CreaturesManager<PlayerView>
    {
        protected override CreatureModel CreateModel()
        {
            var creatureModel = new PlayerModel(100, 10, 10, 2.0f);
            return creatureModel;
        }
    }
}
