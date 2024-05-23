using System.IO;
using MageVsMonsters.Views;

namespace MageVsMonsters.Managers
{
    public class EnemiesManager : CreaturesManager<EnemyView>
    {
        protected override string DefinitionPath { get; } = Path.Combine("Definitions", "Enemies");
        protected override string PrefabsPath { get; } = Path.Combine("Prefabs", "GameObjects", "Creatures", "Enemies");

        protected override void OnInstancesCountChanged(int instancesCount)
        {
            base.OnInstancesCountChanged(instancesCount);

            if (instancesCount < _initialInstancesCount)
            {
                Spawn();
            }
        }
    }
}
