namespace MageVsMonsters.Models
{
    public class EnemyModel : CreatureModel
    {
        public EnemyModel(int health) :
            base(health)
        {
        }
        public EnemyModel(int maxHealth, int damage, int defense, float movementSpeed) :
            base(maxHealth, damage, defense, movementSpeed)
        {
        }
    }
}
