﻿using MageVsMonsters.Models;
using MageVsMonsters.Views;

namespace MageVsMonsters.Managers
{
    public class EnemiesManager : CreaturesManager<EnemyView>
    {
        protected override CreatureModel CreateModel()
        {
            var creatureModel = new EnemyModel(100);
            return creatureModel;
        }
    }
}
