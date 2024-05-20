namespace MageVsMonsters.Models
{
    public class ProjectileModel : IModel
    {
        public int Damage
        {
            get;
            set;
        }

        public ProjectileModel()
        {
            Damage = 0;
        }
        public ProjectileModel(int damage) :
            this()
        {
            Damage = damage;
        }
    }
}
