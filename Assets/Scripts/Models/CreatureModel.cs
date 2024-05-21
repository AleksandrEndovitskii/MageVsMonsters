namespace MageVsMonsters.Models
{
    public class CreatureModel : IModel
    {
        public int Health
        {
            get;
            set;
        }

        public CreatureModel()
        {
            Health = 0;
        }
        public CreatureModel(int health) :
            this()
        {
            Health = health;
        }
    }
}
