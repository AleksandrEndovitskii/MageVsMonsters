namespace MageVsMonsters.Models
{
    public class SpellModel : IModel
    {
        public int Damage
        {
            get;
            private set;
        }

        public SpellModel()
        {
            Damage = 0;
        }
        public SpellModel(int damage) :
            this()
        {
            Damage = damage;
        }
    }
}
