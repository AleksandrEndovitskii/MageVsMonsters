namespace MageVsMonsters.Models
{
    public class CharacterModel : IModel
    {
        public int Health
        {
            get;
            set;
        }

        protected CharacterModel()
        {
            Health = 0;
        }
        public CharacterModel(int health) :
            this()
        {
            Health = health;
        }
    }
}
