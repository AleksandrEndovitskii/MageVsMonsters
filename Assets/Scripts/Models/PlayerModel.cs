namespace MageVsMonsters.Models
{
    public class PlayerModel : CreatureModel
    {
        public PlayerModel(int maxHealth, int damage, int defense, float movementSpeed) :
            base(maxHealth, damage, defense, movementSpeed)
        {
        }
    }
}
