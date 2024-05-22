using MageVsMonsters.JsonObjects;

namespace MageVsMonsters.Models
{
    public class EnemyModel : CreatureModel
    {
        public EnemyModel(CreatureDefinitionJsonObject creatureDefinitionJsonObject) :
            base(creatureDefinitionJsonObject)
        {
        }
    }
}
