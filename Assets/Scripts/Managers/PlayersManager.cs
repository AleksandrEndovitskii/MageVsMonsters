using System.IO;
using System.Linq;
using MageVsMonsters.Models;
using MageVsMonsters.Views;

namespace MageVsMonsters.Managers
{
    public class PlayersManager : CreaturesManager<PlayerView>
    {
        protected override string DefinitionPath { get; } = Path.Combine("Definitions", "Players");
        protected override string PrefabsPath { get; } = Path.Combine("Prefabs", "GameObjects", "Creatures", "Players");

        protected override CreatureModel GetModel(Rarity rarity)
        {
            // temp solution - player have only 1 definition
            var creatureDefinitionJsonObject = _definitionJsonObjects.First();
            var creatureModel = new CreatureModel(creatureDefinitionJsonObject);

            return creatureModel;
        }
    }
}
