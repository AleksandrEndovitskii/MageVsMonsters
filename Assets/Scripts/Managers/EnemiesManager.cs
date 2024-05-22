using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Helpers;
using MageVsMonsters.JsonObjects;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using Newtonsoft.Json;

namespace MageVsMonsters.Managers
{
    public class EnemiesManager : CreaturesManager<EnemyView>
    {
        private static readonly string ENEMY_DEFINITIONS_FILE_NAME = "Definitions" + Path.DirectorySeparatorChar + "Enemies";

        private List<CreatureDefinitionJsonObject> _creatureDefinitionJsonObjects;

        protected override async UniTask Initialize()
        {
            var enemyDefinitionsJson = ResourcesHelper.LoadFromJsonInResources<string>(ENEMY_DEFINITIONS_FILE_NAME);
            _creatureDefinitionJsonObjects = JsonConvert.DeserializeObject<List<CreatureDefinitionJsonObject>>(enemyDefinitionsJson);

            await base.Initialize();
        }

        protected override CreatureModel CreateModel()
        {
            var creatureModel = GetRandomEnemyModel();
            return creatureModel;
        }

        private EnemyModel GetRandomEnemyModel()
        {
            var randomValue = UnityEngine.Random.Range(0, 100);
            var rarity = RarityHelper.GetRarity(randomValue);
            var result = GetEnemyModel(rarity);

            return result;
        }
        private EnemyModel GetEnemyModel(Rarity rarity)
        {
            var creatureDefinitionJsonObject = _creatureDefinitionJsonObjects.FirstOrDefault(em => em.Rarity == rarity);
            var enemyModel = new EnemyModel(creatureDefinitionJsonObject);

            return enemyModel;
        }
    }
}
