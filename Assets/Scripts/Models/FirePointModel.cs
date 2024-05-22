namespace MageVsMonsters.Models
{
    public class FirePointModel : IModel
    {
        public CreatureModel CreatureViewModel
        {
            get;
            private set;
        }

        public FirePointModel(CreatureModel creatureViewModel)
        {
            CreatureViewModel = creatureViewModel;
        }
    }
}
