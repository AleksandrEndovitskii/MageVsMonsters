using System.IO;
using MageVsMonsters.Views;

namespace MageVsMonsters.Managers
{
    public class PlayersManager : CreaturesManager<PlayerView>
    {
        protected override string DefinitionPath { get; } = Path.Combine("Definitions", "Players");
        protected override string PrefabsPath { get; } = Path.Combine("Prefabs", "GameObjects", "Creatures", "Players");

    }
}
